using Bizomet.Data.Configurations;
using Bizomet.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bizomet.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string,
		IdentityUserClaim<string>,
		ApplicationUserRole,
		IdentityUserLogin<string>,
		IdentityRoleClaim<string>,
		IdentityUserToken<string>>
	{
		public DbSet<UserProfile> UserProfile { get; set; }

		public ApplicationDbContext(DbContextOptions options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
			modelBuilder.ApplyConfiguration(new ApplicationRoleConfiguration());
			modelBuilder.ApplyConfiguration(new UserProfileConfiguration());
		}
	}
}