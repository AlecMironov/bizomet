using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Bizomet.Web.Helpers
{
	public interface ITokenService
	{
		List<Claim> GetClaims(IdentityUser user, IEnumerable<string> roles);

		string GenerateAccessToken(IEnumerable<Claim> claims);

		string GenerateRefreshToken();

		ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
	}
}
