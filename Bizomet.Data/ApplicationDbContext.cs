using Bizomet.Data.Configurations;
using Bizomet.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bizomet.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new CompanyConfiguration());
		}

		public DbSet<Company> Companies { get; set; }
	}
}