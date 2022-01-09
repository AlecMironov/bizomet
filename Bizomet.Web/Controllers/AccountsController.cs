using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Bizomet.Data.Models;
using Bizomet.ViewModels;
using Bizomet.Web.JwtFeatures;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bizomet.Web.Controllers
{
	[Route("api/accounts")]
	[ApiController]
	public class AccountsController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IMapper _mapper;
		private readonly JwtHandler _jwtHandler;

		public AccountsController(UserManager<ApplicationUser> userManager, IMapper mapper, JwtHandler jwtHandler)
		{
			_userManager = userManager;
			_mapper = mapper;
			_jwtHandler = jwtHandler;
		}

		[HttpPost("Registration")]
		public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationViewModel userForRegistration)
		{
			if (userForRegistration == null || !ModelState.IsValid)
				return BadRequest();

			var user = _mapper.Map<ApplicationUser>(userForRegistration);

			var result = await _userManager.CreateAsync(user, userForRegistration.Password);
			if (!result.Succeeded) {
				var errors = result.Errors.Select(e => e.Description);

				return BadRequest(new RegistrationResponseViewModel { Errors = errors });
			}

			return StatusCode(201);
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] UserForAuthenticationViewModel userForAuthentication)
		{
			var user = await _userManager.FindByNameAsync(userForAuthentication.Email);

			if (user == null || !await _userManager.CheckPasswordAsync(user, userForAuthentication.Password))
				return Unauthorized(new AuthResponseViewModel { ErrorMessage = "Invalid Authentication" });

			var signingCredentials = _jwtHandler.GetSigningCredentials();
			var claims = _jwtHandler.GetClaims(user);
			var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
			var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

			return Ok(new AuthResponseViewModel { IsAuthSuccessful = true, Token = token });
		}
	}
}
