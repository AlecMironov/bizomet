using System.Text;
using AutoMapper;
using Bizomet.Contracts;
using Bizomet.Core.Helpers;
using Bizomet.Data.DataEncryption;
using Bizomet.Data.Entities;
using Bizomet.Models;
using MailKitMailer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bizomet.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
	public class UserPortfolioController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IRepositoryManager _repositoryManager;
		private readonly IMapper _mapper;
		private readonly IEncryptionProvider _encryptionProvider;
		private readonly IMailClient _mailClient;
		private readonly ILogger<AccountController> _logger;

		public UserPortfolioController(
			UserManager<ApplicationUser> userManager,
			IMapper mapper,
			IRepositoryManager repositoryManager,
			IEncryptionProvider encryptionProvider,
			IMailClient mailClient,
			ILogger<AccountController> logger)
		{
			_userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
			_repositoryManager = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
			_encryptionProvider = encryptionProvider ?? throw new ArgumentNullException(nameof(encryptionProvider));
			_mailClient = mailClient ?? throw new ArgumentNullException(nameof(mailClient));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetAll([FromQuery] int first, int rows, string sortField, int sortOrder)
		{
			if (User == null || User.Identity == null || !User.Identity.IsAuthenticated)
				return Unauthorized("Unauthorized request");

			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			if (user == null)
				return Unauthorized("Unauthorized request");

			var totalRecords = _repositoryManager.UserPortfolio.GetAll(r => r.UserId == user.Id).Count();
			var portfolio = _repositoryManager.UserPortfolio.GetAll(r => r.UserId == user.Id).OrderBy(r => r.Order).Skip(first).Take(rows);
			var result = _mapper.Map<IEnumerable<UserPortfolio>, IEnumerable<UserPortfolioModel>>(portfolio);

			return Ok(new { data = result, total_records = totalRecords });
		}

		[HttpGet("{id}", Name = "GetPortfolio")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetById(string id)
		{
			if (User == null || User.Identity == null || !User.Identity.IsAuthenticated)
				return Unauthorized("Unauthorized request");

			var user = await _userManager.Users.Include(u => u.UserPortfolio).FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
			if (user == null)
				return Unauthorized("Unauthorized request");

			var portfolio = user.UserPortfolio.FirstOrDefault(r => r.Id == Guid.Parse(id));
			if (portfolio == null)
				return BadRequest();

			var result = _mapper.Map<UserPortfolioModel>(portfolio);
			return Ok(result);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> Create([FromBody] UserPortfolioModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			if (User == null || User.Identity == null || !User.Identity.IsAuthenticated)
				return Unauthorized("Unauthorized request");

			try {
				var user = await _userManager.Users.Include(u => u.UserPortfolio).FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
				if (user == null)
					return Unauthorized("Unauthorized request");

				model.Id = Guid.NewGuid().ToString("N");
				model.Order = user.UserPortfolio.Max(r => r.Order) + 1;
				var newPortfolio = _mapper.Map<UserPortfolio>(model);
				newPortfolio.User = user;

				_repositoryManager.UserPortfolio.Create(newPortfolio);
				_repositoryManager.Save();

				return Ok(model);
			}
			catch (Exception e) {
				_logger.LogError(e, $"Something went wrong in the {nameof(Create)}");
				return Problem("Internal Server Error. Please Try Again Later.", statusCode: StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Update(string id, [FromBody] UserPortfolioModel model)
		{
			if (!ModelState.IsValid || model == null || model.Id != id)
				return BadRequest();

			if (User == null || User.Identity == null || !User.Identity.IsAuthenticated)
				return Unauthorized("Unauthorized request");

			try {
				var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
				if (user == null)
					return Unauthorized("Unauthorized request");

				var portfolio = _repositoryManager.UserPortfolio.Get(Guid.Parse(id));
				if (portfolio == null)
					return BadRequest();

				_mapper.Map(model, portfolio);
				portfolio.User = user;
				portfolio.UserId = user.Id;

				_repositoryManager.UserPortfolio.Update(portfolio);
				_repositoryManager.Save();

				return Ok(model);
			}
			catch (Exception e) {
				_logger.LogError(e, $"Something went wrong in the {nameof(Profile)}");
				return Problem("Internal Server Error. Please Try Again Later.", statusCode: StatusCodes.Status500InternalServerError);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(string id)
		{
			if (User == null || User.Identity == null || !User.Identity.IsAuthenticated)
				return Unauthorized("Unauthorized request");

			try {
				var user = await _userManager.Users.Include(u => u.UserPortfolio).FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
				if (user == null)
					return Unauthorized("Unauthorized request");

				var portfolio = user.UserPortfolio.FirstOrDefault(r => r.Id == Guid.Parse(id));
				if (portfolio == null)
					return BadRequest();

				user.UserPortfolio.Remove(portfolio);
				var updateResult = await _userManager.UpdateAsync(user);
				if (!updateResult.Succeeded) {
					var errors = updateResult.Errors.Select(e => e.Description);
					_logger.LogError($"Something went wrong in the {nameof(Update)}; {errors}");
					return Problem($"Something went wrong in the {nameof(Update)}", statusCode: StatusCodes.Status500InternalServerError);
				}

				return new NoContentResult();
			}
			catch (Exception e) {
				_logger.LogError(e, $"Something went wrong in the {nameof(Profile)}");
				return Problem("Internal Server Error. Please Try Again Later.", statusCode: StatusCodes.Status500InternalServerError);
			}
		}
	}
}