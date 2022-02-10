using AutoMapper;
using Bizomet.Contracts;
using Bizomet.Data.DataEncryption;
using Bizomet.Data.Entities;
using Bizomet.Mailer;
using Bizomet.Models;
using Bizomet.Models.MailModels;
using MailKitMailer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

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
		private readonly ILogger<AccountController> _logger;

		public UserProfileController(
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
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Profile()
		{
			try {
				var user = await _userManager.GetUserAsync(User);
				if (user == null)
					return Unauthorized("Unauthorized request");

				var userRoles = await _userManager.GetRolesAsync(user);
				var result = _mapper.Map<UserProfile, UserProfileModel>(user.UserProfile);
				result.Roles = userRoles;

				return Ok(result);
			}
			catch (Exception e) {
				_logger.LogError(e, $"Something went wrong in the {nameof(Profile)}");
				return Problem("Internal Server Error. Please Try Again Later.", statusCode: StatusCodes.Status500InternalServerError);
			}
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> ProfileImage([FromQuery] bool large)
		{
			try {
				var user = await _userManager.GetUserAsync(User);
				if (user == null)
					return Unauthorized("Unauthorized request");

				var result = large ? user.UserProfile.PictureLarge : user.UserProfile.PictureSmall;

				return Ok(new { result });
			}
			catch (Exception e) {
				_logger.LogError(e, $"Something went wrong in the {nameof(Profile)}");
				return Problem("Internal Server Error. Please Try Again Later.", statusCode: StatusCodes.Status500InternalServerError);
			}
		}
	}
}