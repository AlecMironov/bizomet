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
			var user = await _userManager.FindByNameAsync(userModel.Email);
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

			var userRoles = await _userManager.GetRolesAsync(user);
			if (userRoles == null) {
				return Unauthorized("Invalid Authentication");
			}

			var claims = _tokenService.GetClaims(user, userRoles);
			var accessToken = _tokenService.GenerateAccessToken(claims);
			var refreshToken = _tokenService.GenerateRefreshToken();

			user.RefreshToken = refreshToken;
			user.RefreshTokenExpiryTime = DateTime.Now.AddDays(1);

			//user.UserProfile.PictureLarge = "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";
			//user.UserProfile.PictureSmall = "/9j/4AAQSkZJRgABAQAAAQABAAD/4gIoSUNDX1BST0ZJTEUAAQEAAAIYAAAAAAQwAABtbnRyUkdCIFhZWiAAAAAAAAAAAAAAAABhY3NwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAA9tYAAQAAAADTLQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAlkZXNjAAAA8AAAAHRyWFlaAAABZAAAABRnWFlaAAABeAAAABRiWFlaAAABjAAAABRyVFJDAAABoAAAAChnVFJDAAABoAAAAChiVFJDAAABoAAAACh3dHB0AAAByAAAABRjcHJ0AAAB3AAAADxtbHVjAAAAAAAAAAEAAAAMZW5VUwAAAFgAAAAcAHMAUgBHAEIAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFhZWiAAAAAAAABvogAAOPUAAAOQWFlaIAAAAAAAAGKZAAC3hQAAGNpYWVogAAAAAAAAJKAAAA+EAAC2z3BhcmEAAAAAAAQAAAACZmYAAPKnAAANWQAAE9AAAApbAAAAAAAAAABYWVogAAAAAAAA9tYAAQAAAADTLW1sdWMAAAAAAAAAAQAAAAxlblVTAAAAIAAAABwARwBvAG8AZwBsAGUAIABJAG4AYwAuACAAMgAwADEANv/bAEMAAwICAgICAwICAgMDAwMEBgQEBAQECAYGBQYJCAoKCQgJCQoMDwwKCw4LCQkNEQ0ODxAQERAKDBITEhATDxAQEP/bAEMBAwMDBAMECAQECBALCQsQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEP/AABEIADIAMgMBIgACEQEDEQH/xAAdAAACAgMAAwAAAAAAAAAAAAAABgcIBAUJAQID/8QANRAAAQMDAgQDBgMJAAAAAAAAAQIDBAUGEQAhBxIxQRMiUQgUFjJhcRVCkRc0NkNUc4GUsf/EABoBAAMAAwEAAAAAAAAAAAAAAAQFBgABAwL/xAArEQABBAADBgUFAAAAAAAAAAABAAIDBBESIQUiMTJRkRRhgaHREzNBUvH/2gAMAwEAAhEDEQA/AOaz1MjwQZC0/IAnB2Ow1l0pbUoBqUg4KSpJAzv6aZkWDPvCQ9UpldpNNiPO+MsreGQCckJAzrT3LHZoVdRTLXebqyGW8BZQQFKPfGe2P015tWoZGmGM6ouvUnjwmkGiY7S4O3lf8OXJtiIrwYrqUuPY2G2wydsnUjQuDHGCg01cuFHgvKjJ8yFRGiogD1Kd9SBwr4h2jYfDWJGn1J9l9gJVUgw0MIkuDJBJ2OBtk9htp3+PGq3TFS7fne9MLbKifzJJG+QPpqOk2jYjflaNAq6HY1aSIPecXEYqpNwcUeKdqspXKhUpTKlFHMunt5Ch2ONLrntC366d2qSB6JgoA04cVaNU5zU6UGVuRk8ynOUZCPQn01AWqSjKLEeLuKlb9c1pcBwKkj9vd7f09J/0k6NRvo0bkb0QOJTAmtuP0EUxKfOFBKQOpGvFBXKhyGlJcW254pBwcEbaz+F1ot3pe8G3JTi2m3isrKVBKvKknAz3zqf6TwYt2hXR8MTx46H45lQ3l8q1lQOHEE7bgEEfQ/TQO0b7Itx3HBF1ISd5NRvy3PdYFAiWXD9x5Euu+8rSEPkpCSs9yogDB7f5zppcuimNwDGoFIiU4EAqQhPnVt3PcffS3UaKbQhmQ3SpUyKwykF1GChoZwOdIyQOgydjsNJNKqdSq9YTFixnlF5wJbbQCSo52AA66lPpibeb/Sr5l1oibqOHZe/FSKzAsisXGiS2rLAiBgYUUOOEgrz1TscY7gA/aq2uq1k8Bbar9j1C379golLrrJDrIIJjoAyFJI/ODggjIBGBkZzX2/fYRbtKNJqcNx2qQmCsqMJLji20gZBUgZOSPTIzkZ09oyeAjyTA5j09lKbWeL0odFytGHyqV6NPci3aIzIdZ+Fq4eRak55Fjofto028QP1Pt8pHk81ueCEF1Mup3a00lyZSC1JjlRPmUFZcT17pyO/XXQe0LAsi56FTOI92RwDT8zo7cVXIFpKCClwkZIOegwTgHI1UHhJw5n0+K1GeD6lyFAKaGMbntj11cmm2vPrUKlWdT0uRqXBbDcpWdgAAFYOdyQOUHtkkdBqWuvZbsmQfjROqzHQxhpSDeNZp0qJKuOu0o0u2BztQqbHQEmYstrIJ7qPKDjJ6kbjSz7J7nD65m6rU6X4n43EcPjtShkxmTunkOACANlHAIIOcAjWy9py+bTtuEmkKUXXWXExKbHZzzeKlaS47gdcFIQB6hfYHWx9mG12araz71Rpa4brspxTjXMAshR5yFcp7hQyD2OD104pRMgizuHmhp5HPfkadFOlKqjTFPeqrBL3jsEs78vMgDIO+MA9c+h1lWxX5MWmiY+4lRl5cIzkb7jGe2DpJ4gvsT6g3bdBx76ttDU5xCv3eKVbg+hWMgd8ZPpn5Lq6oAlJcewy2MpBOwwMbaUSyvkeXu6oxjQxuUJyeuSKp1ZMNjdRPyD1+2jUROXSouKPjN9T/ADdGszv6rWiiWy/4pp/9wf8ARq4dgE/BniZ8/K55u/66NGhoebsu55Qud/HZ11zjCfEdWrwaNGcbyonkUpIKlD0JJJJ7knVtfZO83D9Dqt1r8QqUepOepOjRqmm+16BKI+f1SxwrkyH7vvpx59xxf4g8OZSiTsrA3PoOmtrcrjnuUg86vlPf6jRo0kk5uyZNS8lKeUeUdPTRo0a2uK//2Q==";
			var result = await _userManager.UpdateAsync(user);
			if (!result.Succeeded) {
				var errors = result.Errors.Select(e => e.Description);
				_logger.LogError($"Something went wrong in the {nameof(Login)}; {errors}");
				return Problem($"Something went wrong in the {nameof(Login)}", statusCode: StatusCodes.Status500InternalServerError);
			}

			await _userManager.ResetAccessFailedCountAsync(user);

			return Ok(
				new AuthResponseModel {
					id = user.Id,
					userName = user.UserName,
					firstName = _encryptionProvider.Decrypt(user.UserProfile.FirstName),
					lastName = _encryptionProvider.Decrypt(user.UserProfile.LastName),
					roles = userRoles,
					token = accessToken,
					refreshToken = refreshToken
				});
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