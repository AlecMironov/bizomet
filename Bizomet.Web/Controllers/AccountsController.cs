using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Bizomet.Data.Entities;
using Bizomet.Models;
using Bizomet.Web.Helpers;
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
		public async Task<IActionResult> Registration([FromBody] UserRegistrationModel userModel)
		{
			if (userModel == null || !ModelState.IsValid)
				return BadRequest();

			var user = _mapper.Map<ApplicationUser>(userModel);

			var result = await _userManager.CreateAsync(user, userModel.Password);
			if (!result.Succeeded) {
				var errors = result.Errors.Select(e => e.Description);

				return BadRequest(new RegistrationResponseModel { Errors = errors });
			}

			return StatusCode(201);
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] UserAuthenticationModel userModel)
		{
			var user = await _userManager.FindByNameAsync(userModel.Email);

			if (user == null || !await _userManager.CheckPasswordAsync(user, userModel.Password))
				return Unauthorized(new AuthResponseModel { ErrorMessage = "Invalid Authentication" });

			var signingCredentials = _jwtHandler.GetSigningCredentials();
			var claims = _jwtHandler.GetClaims(user);
			var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
			var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

			return Ok(new AuthResponseModel { IsAuthSuccessful = true, Token = token });
		}
	}
}