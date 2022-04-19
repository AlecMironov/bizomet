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
	[Route("api/[controller]/[action]")]
	[ApiController]
	[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
	public class UserProfileController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IRepositoryManager _repositoryManager;
		private readonly IMapper _mapper;
		private readonly IEncryptionProvider _encryptionProvider;
		private readonly IMailClient _mailClient;
		private readonly ILogger<UserProfileController> _logger;

		public UserProfileController(
			UserManager<ApplicationUser> userManager,
			IMapper mapper,
			IRepositoryManager repositoryManager,
			IEncryptionProvider encryptionProvider,
			IMailClient mailClient,
			ILogger<UserProfileController> logger)
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
		public async Task<IActionResult> Profile()
		{
			if (User == null || User.Identity == null || !User.Identity.IsAuthenticated)
				return Unauthorized("Unauthorized request");

			var user = await _userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
			if (user == null)
				return Unauthorized("Unauthorized request");

			var result = _mapper.Map<UserProfileModel>(user.UserProfile);
			return Ok(result);
		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Update()
		{
			string result;
			using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8)) {
				result = await reader.ReadToEndAsync();
			}

			if (string.IsNullOrEmpty(result) || result == "{}")
				return NoContent();

			if (User == null || User.Identity == null || !User.Identity.IsAuthenticated)
				return Unauthorized("Unauthorized request");

			try {
				var user = await _userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
				if (user == null)
					return Unauthorized("Unauthorized request");

				var userProfileModel = _mapper.Map<UserProfileModel>(user.UserProfile);
				SerializationHelper.JsonPopulate(result, userProfileModel);
				user.UserProfile = _mapper.Map<UserProfile>(userProfileModel);

				var updateResult = await _userManager.UpdateAsync(user);
				if (!updateResult.Succeeded) {
					var errors = updateResult.Errors.Select(e => e.Description);
					_logger.LogError($"Something went wrong in the {nameof(Update)}; {errors}");
					return Problem($"Something went wrong in the {nameof(Update)}", statusCode: StatusCodes.Status500InternalServerError);
				}

				var userRoles = await _userManager.GetRolesAsync(user);
				if (userRoles.Contains("Administrator"))
					((List<string>) userProfileModel.Roles).Add("Administrator");
				bool areEqual = Enumerable.SequenceEqual(userRoles.OrderBy(e => e), userProfileModel.Roles.OrderBy(e => e));
				if (!areEqual) {
					await _userManager.RemoveFromRolesAsync(user, userRoles);
					await _userManager.AddToRolesAsync(user, userProfileModel.Roles);
				}

				return Ok();
			}
			catch (Exception e) {
				_logger.LogError(e, $"Something went wrong in the {nameof(Profile)}");
				return Problem("Internal Server Error. Please Try Again Later.", statusCode: StatusCodes.Status500InternalServerError);
			}
		}

		[HttpGet]
		[AllowAnonymous]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		public async Task<IActionResult> ValidateUserPublicName([FromQuery] string publicName)
		{
			var result = await _repositoryManager.UserProfile.Exists(r => r.PublicName == publicName);
			if (result)
				return Conflict("Public Name is already taken");

			return Ok(); //ok means publicName does not exists.
		}
	}
}
