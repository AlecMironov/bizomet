using AutoMapper;
using Bizomet.Contracts;
using Bizomet.Data.Entities;
using Bizomet.Models;
using Bizomet.Web.Helpers;
using Bizomet.Web.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Bizomet.Web.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class TokenController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IRepositoryManager _repositoryManager;
		private readonly IMapper _mapper;
		private readonly ITokenService _tokenService;
		private readonly ILogger<TokenController> _logger;

		public TokenController(UserManager<ApplicationUser> userManager, IMapper mapper, ITokenService tokenService, IRepositoryManager repositoryManager, ILogger<TokenController> logger)
		{
			_userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
			_repositoryManager = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
			_tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Refresh([FromBody] TokenApiModel tokenApiModel)
		{
			if (tokenApiModel is null) {
				return BadRequest("Invalid client request");
			}

			string accessToken = tokenApiModel.accessToken;
			string refreshToken = tokenApiModel.refreshToken;

			var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
			var userId = principal.GetLoggedInUserId<string>();
			if (string.IsNullOrEmpty(userId))
				return BadRequest("Invalid client request");
			
			var user = await _userManager.Users.Include(u => u.RefreshTokens).FirstOrDefaultAsync(u => u.Id == userId && u.RefreshTokens.Any(t => t.Token == refreshToken));
			if (user == null) {
				return BadRequest("Invalid client request");
			}
			var refreshTokenEntity = user.RefreshTokens.Single(x => x.Token == refreshToken);
			if (!refreshTokenEntity.IsActive) {
				return BadRequest("Invalid client request");
			}

			var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
			var newRefreshToken = _tokenService.GenerateRefreshToken();

			refreshTokenEntity.Revoked = DateTime.UtcNow;
			refreshTokenEntity.RevokedByIp = getClientIP();
			refreshTokenEntity.ReplacedByToken = newRefreshToken;

			user.RefreshTokens.Add(new RefreshToken {
				Token = newRefreshToken,
				Expires = DateTime.UtcNow.AddDays(7),
				Created = DateTime.UtcNow,
				CreatedByIp = getClientIP()
			});
			var result = await _userManager.UpdateAsync(user);

			if (!result.Succeeded) {
				var errors = result.Errors.Select(e => e.Description);
				_logger.LogError($"Something went wrong in the {nameof(Refresh)}; {errors}");
				return Problem($"Something went wrong in the {nameof(Refresh)}", statusCode: 500);
			}

			return Ok(new { accessToken = newAccessToken, refreshToken = newRefreshToken });
		}

		//[HttpPost]
		//[Authorize]
		//[ProducesResponseType(StatusCodes.Status204NoContent)]
		//[ProducesResponseType(StatusCodes.Status400BadRequest)]
		//[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		//public async Task<IActionResult> Revoke()
		//{
		//	try {
		//		var user = _userManager.Users.Include(u => u.RefreshTokens).FirstOrDefault(u => u.UserName == User.Identity.Name);
		//		if (user == null)
		//			return BadRequest("Invalid client request");

		//		var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

		//		// return false if no user found with token
		//		if (user == null) return false;

		//		var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

		//		// return false if token is not active
		//		if (!refreshToken.IsActive) return false;

		//		// revoke token and save
		//		refreshToken.Revoked = DateTime.UtcNow;
		//		refreshToken.RevokedByIp = ipAddress;
		//		_context.Update(user);
		//		_context.SaveChanges();


		//		user.RefreshToken = null;
		//		user.RefreshTokenExpiryTime = null;
		//		var result = await _userManager.UpdateAsync(user);
		//		if (result.Succeeded) {
		//			return NoContent();
		//		}
		//		else {
		//			var errors = result.Errors.Select(e => e.Description);
		//			_logger.LogError($"Something went wrong in the {nameof(Revoke)}; {errors}");
		//			return Problem($"Something went wrong in the {nameof(Revoke)}", statusCode: StatusCodes.Status500InternalServerError);
		//		}
		//	}
		//	catch (Exception e) {
		//		_logger.LogError(e, $"Something went wrong in the {nameof(Revoke)}");
		//		return Problem($"Something went wrong in the {nameof(Revoke)}", statusCode: StatusCodes.Status500InternalServerError);
		//	}
		//}

		private string getClientIP()
		{
			if (Request.Headers.ContainsKey("X-Forwarded-For"))
				return Request.Headers["X-Forwarded-For"];
			else
				return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
		}
	}
}
