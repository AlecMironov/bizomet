using Bizomet.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bizomet.Data.Configurations
{
	public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
	{
		public void Configure(EntityTypeBuilder<ApplicationRole> builder)
		{
			// Each Role can have many entries in the UserRole join table
			builder.HasMany(e => e.UserRoles)
				.WithOne(e => e.Role)
				.HasForeignKey(ur => ur.RoleId)
				.IsRequired();

			builder.HasData(
			new IdentityRole {
				Name = "Administrator",
				NormalizedName = "ADMINISTRATOR"
			},
			new IdentityRole {
				Name = "Talent",
				NormalizedName = "TALENT"
			},
			new IdentityRole {
				Name = "Lifter",
				NormalizedName = "LIFTER"
			},
			new IdentityRole {
				Name = "MediaAssistant",
				NormalizedName = "MEDIAASSISTANT"
			},
			new IdentityRole {
				Name = "Promoter",
				NormalizedName = "PROMOTER"
			});
		}
	}
}
