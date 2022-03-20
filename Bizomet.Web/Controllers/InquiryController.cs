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
	public class InquiryController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IRepositoryManager _repositoryManager;
		private readonly IMapper _mapper;
		private readonly IEncryptionProvider _encryptionProvider;
		private readonly IMailClient _mailClient;
		private readonly ILogger<InquiryController> _logger;

		public InquiryController(
			UserManager<ApplicationUser> userManager,
			IMapper mapper,
			IRepositoryManager repositoryManager,
			IEncryptionProvider encryptionProvider,
			IMailClient mailClient,
			ILogger<InquiryController> logger)
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

			var totalRecords = _repositoryManager.Inquiries.GetAll().Count();
			var inquiries = _repositoryManager.Inquiries.GetAll().OrderByDescending(r => r.RequestDate).Skip(first).Take(rows);
			var result = _mapper.Map<IEnumerable<Inquiry>, IEnumerable<InquiryModel>>(inquiries);

			return Ok(new { data = result, total_records = totalRecords });
		}

		[HttpGet("userinquiries")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetUserInquiries([FromQuery] int first, int rows, string sortField, int sortOrder)
		{
			if (User == null || User.Identity == null || !User.Identity.IsAuthenticated)
				return Unauthorized("Unauthorized request");

			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			if (user == null)
				return Unauthorized("Unauthorized request");

			var totalRecords = _repositoryManager.Inquiries.GetAll(r => r.UserId == user.Id).Count();
			var inquiries = _repositoryManager.Inquiries.GetAll(r => r.UserId == user.Id).OrderByDescending(r => r.RequestDate).Skip(first).Take(rows);
			var result = _mapper.Map<IEnumerable<Inquiry>, IEnumerable<InquiryModel>>(inquiries);

			return Ok(new { data = result, total_records = totalRecords });
		}

		[HttpGet("{id}", Name = "GetInquiry")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetById(string id)
		{
			if (User == null || User.Identity == null || !User.Identity.IsAuthenticated)
				return Unauthorized("Unauthorized request");

			var user = await _userManager.Users.Include(u => u.Inquiries).FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
			if (user == null)
				return Unauthorized("Unauthorized request");

			var inquiry = user.Inquiries.FirstOrDefault(r => r.Id == Guid.Parse(id));
			if (inquiry == null)
				return BadRequest();

			var result = _mapper.Map<InquiryModel>(inquiry);
			return Ok(result);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> Create([FromBody] InquiryModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			if (User == null || User.Identity == null || !User.Identity.IsAuthenticated)
				return Unauthorized("Unauthorized request");

			try {
				var user = await _userManager.Users.Include(u => u.Inquiries).FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
				if (user == null)
					return Unauthorized("Unauthorized request");

				model.Id = Guid.NewGuid().ToString("N");
				var inquiry = _mapper.Map<Inquiry>(model);
				inquiry.User = user;

				_repositoryManager.Inquiries.Create(inquiry);
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
		public async Task<IActionResult> Update(string id, [FromBody] InquiryModel model)
		{
			if (!ModelState.IsValid || model == null || model.Id != id)
				return BadRequest();

			if (User == null || User.Identity == null || !User.Identity.IsAuthenticated)
				return Unauthorized("Unauthorized request");

			try {
				var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
				if (user == null)
					return Unauthorized("Unauthorized request");

				var inquiry = _repositoryManager.Inquiries.Get(Guid.Parse(id));
				if (inquiry == null)
					return BadRequest();

				_mapper.Map(model, inquiry);
				inquiry.User = user;
				inquiry.UserId = user.Id;

				_repositoryManager.Inquiries.Update(inquiry);
				_repositoryManager.Save();

				return Ok(model);
			}
			catch (Exception e) {
				_logger.LogError(e, $"Something went wrong in the {nameof(Update)}");
				return Problem("Internal Server Error. Please Try Again Later.", statusCode: StatusCodes.Status500InternalServerError);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(string id)
		{
			if (User == null || User.Identity == null || !User.Identity.IsAuthenticated)
				return Unauthorized("Unauthorized request");

			try {
				var user = await _userManager.Users.Include(u => u.Inquiries).FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
				if (user == null)
					return Unauthorized("Unauthorized request");

				var inquiry = user.Inquiries.FirstOrDefault(r => r.Id == Guid.Parse(id));
				if (inquiry == null)
					return BadRequest();

				user.Inquiries.Remove(inquiry);
				var updateResult = await _userManager.UpdateAsync(user);
				if (!updateResult.Succeeded) {
					var errors = updateResult.Errors.Select(e => e.Description);
					_logger.LogError($"Something went wrong in the {nameof(Delete)}; {errors}");
					return Problem($"Something went wrong in the {nameof(Delete)}", statusCode: StatusCodes.Status500InternalServerError);
				}

				return new NoContentResult();
			}
			catch (Exception e) {
				_logger.LogError(e, $"Something went wrong in the {nameof(Delete)}");
				return Problem("Internal Server Error. Please Try Again Later.", statusCode: StatusCodes.Status500InternalServerError);
			}
		}
	}
}