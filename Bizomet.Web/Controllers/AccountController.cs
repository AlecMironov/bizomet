using AutoMapper;
using Bizomet.Contracts;
using Bizomet.Data.DataEncryption;
using Bizomet.Data.Entities;
using Bizomet.Mailer;
using Bizomet.Models;
using Bizomet.Models.MailModels;
using Bizomet.Web.Helpers;
using MailKitMailer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

namespace Bizomet.Web.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IRepositoryManager _repositoryManager;
		private readonly IMapper _mapper;
		private readonly ITokenService _tokenService;
		private readonly IEncryptionProvider _encryptionProvider;
		private readonly IMailClient _mailClient;
		private readonly ILogger<AccountController> _logger;

		public AccountController(
			UserManager<ApplicationUser> userManager,
			IMapper mapper,
			ITokenService tokenService,
			IRepositoryManager repositoryManager,
			IEncryptionProvider encryptionProvider,
			IMailClient mailClient,
			ILogger<AccountController> logger)
		{
			_userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
			_repositoryManager = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
			_tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
			_encryptionProvider = encryptionProvider ?? throw new ArgumentNullException(nameof(encryptionProvider));
			_mailClient = mailClient ?? throw new ArgumentNullException(nameof(mailClient));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status202Accepted)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Register([FromBody] UserRegistrationModel userModel)
		{
			_logger.LogInformation($"Registration attempt for {userModel.Email}");

			if (userModel == null || !ModelState.IsValid)
				return BadRequest(ModelState);

			try {
				var user = _mapper.Map<ApplicationUser>(userModel);
				var result = await _userManager.CreateAsync(user, userModel.Password);

				if (result.Succeeded) {
					await _userManager.AddToRoleAsync(user, "Talent");

					var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
					var param = new Dictionary<string, string?>
					{
						{"token", token },
						{"email", user.Email }
					};
					var callback = QueryHelpers.AddQueryString(userModel.ClientURI, param);

					var emailModel = new ConfirmRegistrationModel() {
						FirstName = _encryptionProvider.Decrypt(user.UserProfile.FirstName),
						LastName = _encryptionProvider.Decrypt(user.UserProfile.LastName),
						UserName = user.UserName,
						Email = user.Email,
						Date = DateTime.Today,
						ConfirmationLink = callback
					};
					await this._mailClient.SendAsync<IBizometMailer>(x => x.ConfirmRegistrationMail(emailModel));
				}
				else {
					var errors = result.Errors.Select(e => e.Description);
					return BadRequest(new RegistrationResponseModel { Errors = errors });
				}

				return Accepted();
			}
			catch (Exception ex) {
				_logger.LogError(ex, $"Something Went Wrong in the {nameof(Register)}");
				return Problem($"Something Went Wrong in the {nameof(Register)}", statusCode: StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status423Locked)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Login([FromBody] UserAuthenticationModel userModel)
		{
			//var user = await _userManager.FindByNameAsync(userModel.Email);
			var user = _userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).FirstOrDefault(u => u.Email == userModel.Email);
			if (user == null)
				return Unauthorized("Invalid Authentication");

			if (!await _userManager.IsEmailConfirmedAsync(user))
				return Unauthorized("Email is not confirmed yet");

			if (await _userManager.IsLockedOutAsync(user)) {
				return Problem("The account is locked out. Try again later or reset your password.", statusCode: StatusCodes.Status423Locked);
			}

			if (!await _userManager.CheckPasswordAsync(user, userModel.Password)) {
				await _userManager.AccessFailedAsync(user);

				if (await _userManager.IsLockedOutAsync(user)) {
					var emailModel = new ResetPasswordEmailModel() {
						FirstName = _encryptionProvider.Decrypt(user.UserProfile.FirstName),
						LastName = _encryptionProvider.Decrypt(user.UserProfile.LastName),
						UserName = user.UserName,
						Email = user.Email,
						Date = DateTime.Today,
						ResetPasswordLink = userModel.clientURI
					};
					await this._mailClient.SendAsync<IBizometMailer>(x => x.AccountIsLockedOutMail(emailModel));

					return Problem("The account is locked out. Try again later or reset your password.", statusCode: StatusCodes.Status423Locked);
				}

				return Unauthorized("Invalid Authentication");
			}

			var claims = _tokenService.GetClaims(user, user.UserRoles.Select(r => r.Role.Name));
			var accessToken = _tokenService.GenerateAccessToken(claims);
			var refreshToken = _tokenService.GenerateRefreshToken();

			user.RefreshToken = refreshToken;
			user.RefreshTokenExpiryTime = DateTime.Now.AddDays(1);

			var result = await _userManager.UpdateAsync(user);
			if (!result.Succeeded) {
				var errors = result.Errors.Select(e => e.Description);
				_logger.LogError($"Something went wrong in the {nameof(Login)}; {errors}");
				return Problem($"Something went wrong in the {nameof(Login)}", statusCode: StatusCodes.Status500InternalServerError);
			}

			await _userManager.ResetAccessFailedCountAsync(user);

			var response = _mapper.Map<AuthResponseModel>(user);
			response.token = accessToken;
			response.refreshToken = refreshToken;

			return Ok(response);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null)
				return BadRequest("Invalid Request");

			var token = await _userManager.GeneratePasswordResetTokenAsync(user);
			var param = new Dictionary<string, string?>
			{
				{"token", token },
				{"email", model.Email }
			};
			var callback = QueryHelpers.AddQueryString(model.ClientURI, param);
			var emailModel = new ResetPasswordEmailModel() {
				FirstName = _encryptionProvider.Decrypt(user.UserProfile.FirstName),
				LastName = _encryptionProvider.Decrypt(user.UserProfile.LastName),
				UserName = user.UserName,
				Email = user.Email,
				Date = DateTime.Today,
				ResetPasswordLink = callback
			};
			await this._mailClient.SendAsync<IBizometMailer>(x => x.ResetPasswordMail(emailModel));

			_logger.LogDebug($"[Forgot Password] request sent to {model.Email}");

			return Ok();
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null)
				return BadRequest("Invalid Request");

			var resetPassResult = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
			if (!resetPassResult.Succeeded) {
				var errors = resetPassResult.Errors.Select(e => e.Description);
				return BadRequest(new { Errors = errors });
			}

			await _userManager.SetLockoutEndDateAsync(user, null);

			return Ok();
		}

		[HttpPost]
		[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Logout()
		{
			try {
				var user = await _userManager.GetUserAsync(User);
				if (user == null)
					return BadRequest("Invalid client request");

				user.RefreshToken = null;
				user.RefreshTokenExpiryTime = null;
				var result = await _userManager.UpdateAsync(user);
				if (result.Succeeded) {
					return Ok(result);
				}
				else {
					var errors = result.Errors.Select(e => e.Description);
					_logger.LogError($"Something went wrong in the {nameof(Logout)}; {errors}");
					return Problem($"Something went wrong in the {nameof(Logout)}", statusCode: StatusCodes.Status500InternalServerError);
				}
			}
			catch (Exception e) {
				_logger.LogError(e, $"Something went wrong in the {nameof(Logout)}");
				return Problem($"Something went wrong in the {nameof(Logout)}", statusCode: StatusCodes.Status500InternalServerError);
			}
		}

		[HttpGet]
		[AllowAnonymous]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
		{
			if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
				return BadRequest("Invalid Email Confirmation Request");

			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
				return BadRequest("Invalid Email Confirmation Request");

			var confirmResult = await _userManager.ConfirmEmailAsync(user, token);
			if (!confirmResult.Succeeded)
				return BadRequest("Invalid Email Confirmation Request");

			return Ok();
		}
	}
}