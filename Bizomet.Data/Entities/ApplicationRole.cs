using Microsoft.AspNetCore.Identity;

namespace Bizomet.Data.Entities
{
	public class ApplicationRole : IdentityRole
	{
		public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
	}
}