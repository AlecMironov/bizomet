using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bizomet.Data.Configurations
{
	public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
	{
		public void Configure(EntityTypeBuilder<IdentityRole> builder)
		{
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
