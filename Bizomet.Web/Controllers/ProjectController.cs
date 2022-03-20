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
	public class ProjectController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IRepositoryManager _repositoryManager;
		private readonly IMapper _mapper;
		private readonly IEncryptionProvider _encryptionProvider;
		private readonly IMailClient _mailClient;
		private readonly ILogger<ProjectController> _logger;

		public ProjectController(
			UserManager<ApplicationUser> userManager,
			IMapper mapper,
			IRepositoryManager repositoryManager,
			IEncryptionProvider encryptionProvider,
			IMailClient mailClient,
			ILogger<ProjectController> logger)
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

			var totalRecords = _repositoryManager.Projects.GetAll().Count();
			var projects = _repositoryManager.Projects.GetAll().OrderByDescending(r => r.RequestDate).Skip(first).Take(rows);
			var result = _mapper.Map<IEnumerable<Project>, IEnumerable<ProjectModel>>(projects);

			return Ok(new { data = result, total_records = totalRecords });
		}

		[HttpGet("userprojects")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetUserProjects([FromQuery] int first, int rows, string sortField, int sortOrder)
		{
			if (User == null || User.Identity == null || !User.Identity.IsAuthenticated)
				return Unauthorized("Unauthorized request");

			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			if (user == null)
				return Unauthorized("Unauthorized request");

			var totalRecords = _repositoryManager.Projects.GetAll(r => r.UserId == user.Id).Count();
			var projects = _repositoryManager.Projects.GetAll(r => r.UserId == user.Id).OrderByDescending(r => r.RequestDate).Skip(first).Take(rows);
			var result = _mapper.Map<IEnumerable<Project>, IEnumerable<ProjectModel>>(projects);

			return Ok(new { data = result, total_records = totalRecords });
		}

		[HttpGet("{id}", Name = "GetProject")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetById(string id)
		{
			if (User == null || User.Identity == null || !User.Identity.IsAuthenticated)
				return Unauthorized("Unauthorized request");

			var user = await _userManager.Users.Include(u => u.Projects).FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
			if (user == null)
				return Unauthorized("Unauthorized request");

			var project = user.Projects.FirstOrDefault(r => r.Id == Guid.Parse(id));
			if (project == null)
				return BadRequest();

			var result = _mapper.Map<ProjectModel>(project);
			return Ok(result);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> Create([FromBody] ProjectModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			if (User == null || User.Identity == null || !User.Identity.IsAuthenticated)
				return Unauthorized("Unauthorized request");

			try {
				var user = await _userManager.Users.Include(u => u.Projects).FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
				if (user == null)
					return Unauthorized("Unauthorized request");

				model.Id = Guid.NewGuid().ToString("N");
				var project = _mapper.Map<Project>(model);
				project.User = user;

				_repositoryManager.Projects.Create(project);
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
		public async Task<IActionResult> Update(string id, [FromBody] ProjectModel model)
		{
			if (!ModelState.IsValid || model == null || model.Id != id)
				return BadRequest();

			if (User == null || User.Identity == null || !User.Identity.IsAuthenticated)
				return Unauthorized("Unauthorized request");

			try {
				var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
				if (user == null)
					return Unauthorized("Unauthorized request");

				var project = _repositoryManager.Projects.Get(Guid.Parse(id));
				if (project == null)
					return BadRequest();

				_mapper.Map(model, project);
				project.User = user;
				project.UserId = user.Id;

				_repositoryManager.Projects.Update(project);
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
				var user = await _userManager.Users.Include(u => u.Projects).FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
				if (user == null)
					return Unauthorized("Unauthorized request");

				var project = user.Projects.FirstOrDefault(r => r.Id == Guid.Parse(id));
				if (project == null)
					return BadRequest();

				user.Projects.Remove(project);
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