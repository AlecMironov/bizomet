using Bizomet.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bizomet.Data.Configurations
{
	public class CompanyConfiguration : IEntityTypeConfiguration<Company>
	{
		public void Configure(EntityTypeBuilder<Company> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Name).IsRequired().HasMaxLength(60);
			builder.Property(x => x.Address).IsRequired().HasMaxLength(60);

			builder.HasData
			(
				new Company {
					Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
					Name = "IT_Solutions Ltd",
					Address = "583 Wall Dr. Gwynn Oak, MD 21207",
					Country = "USA"
				},
				new Company {
					Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
					Name = "Admin_Solutions Ltd",
					Address = "312 Forest Avenue, BF 923",
					Country = "USA"
				}
			);
		}
	}
}
