using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Bizomet.Web.Helpers
{
	public class TokenService : ITokenService
	{
		private readonly JwtSettings _jwtSettings;

		public TokenService(JwtSettings jwtSettings)
		{
			_jwtSettings = jwtSettings;
		}

		public List<Claim> GetClaims(IdentityUser user, IEnumerable<string> roles)
		{
			var claims = new List<Claim>()
			{
				new Claim("Id", user.Id),
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.NameIdentifier, user.Id),
				new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
			};

			foreach (var role in roles) {
				claims.Add(new Claim(ClaimTypes.Role, role));
			}

			return claims;
		}

		public string GenerateAccessToken(IEnumerable<Claim> claims)
		{
			var key = Encoding.UTF8.GetBytes(_jwtSettings.IssuerSigningKey);
			var tokenOptions = new JwtSecurityToken(
				issuer: _jwtSettings.ValidIssuer,
				audience: _jwtSettings.ValidAudience,
				claims: claims,
				expires: new DateTimeOffset(DateTime.UtcNow.AddDays(1)).DateTime,
				signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
			);

			var result = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
			return result;
		}

		public string GenerateRefreshToken()
		{
			var randomNumber = new byte[32];
			using (var rng = RandomNumberGenerator.Create()) {
				rng.GetBytes(randomNumber);
				return Convert.ToBase64String(randomNumber);
			}
		}

		public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
		{
			var tokenValidationParameters = new TokenValidationParameters {
				ValidateAudience = true, //you might want to validate the audience and issuer depending on your use case
				ValidateIssuer = true,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.IssuerSigningKey)),
				ValidateLifetime = false, //here we are saying that we don't care about the token's expiration date
				ValidAudience = _jwtSettings.ValidAudience,
				ValidIssuer = _jwtSettings.ValidIssuer
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			SecurityToken securityToken;
			var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
			var jwtSecurityToken = securityToken as JwtSecurityToken;

			if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
				throw new SecurityTokenException("Invalid token");

			return principal;
		}
	}
}
