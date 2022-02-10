using AutoMapper;
using Bizomet.Contracts;
using Bizomet.Data.Entities;
using Bizomet.Models;
using Bizomet.Web.Helpers;
using Bizomet.Web.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
		private readonly ILogger<AccountController> _logger;

		public TokenController(UserManager<ApplicationUser> userManager, IMapper mapper, ITokenService tokenService, IRepositoryManager repositoryManager, ILogger<AccountController> logger)
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
			
			var user = await _userManager.FindByIdAsync(userId);
			if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now) {
				return BadRequest("Invalid client request");
			}

			var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
			var newRefreshToken = _tokenService.GenerateRefreshToken();
			user.RefreshToken = newRefreshToken;
			var result = await _userManager.UpdateAsync(user);

			if (!result.Succeeded) {
				var errors = result.Errors.Select(e => e.Description);
				_logger.LogError($"Something went wrong in the {nameof(Refresh)}; {errors}");
				return Problem($"Something went wrong in the {nameof(Refresh)}", statusCode: 500);
			}

			return Ok(new { accessToken = newAccessToken, refreshToken = newRefreshToken });
		}
	}
}
