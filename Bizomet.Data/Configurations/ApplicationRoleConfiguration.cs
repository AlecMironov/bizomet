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
				Id = "8742075e-7145-4bd7-8215-814467809dc2",
				Name = "Administrator",
				NormalizedName = "ADMINISTRATOR"
			},
			new IdentityRole {
				Id = "69571a28-cb0d-4fe6-8176-3bffad6c1510",
				Name = "Talent",
				NormalizedName = "TALENT"
			},
			new IdentityRole {
				Id = "347ac56d-9576-4f4a-81be-674b4a3a9d0b",
				Name = "Uplifter",
				NormalizedName = "UPLIFTER"
			},
			new IdentityRole {
				Id = "7bd1c590-9eed-44e9-a60c-6e7de0db8f01",
				Name = "MediaAssistant",
				NormalizedName = "MEDIAASSISTANT"
			},
			new IdentityRole {
				Id = "7e6619f8-b336-4f3e-826a-5ce96cef872d",
				Name = "Promoter",
				NormalizedName = "PROMOTER"
			},
			new IdentityRole {
				Id = "8832961e-a631-445b-9d86-b93f9b4c767b",
				Name = "Producer",
				NormalizedName = "PRODUCER"
			});
		}
	}
}
