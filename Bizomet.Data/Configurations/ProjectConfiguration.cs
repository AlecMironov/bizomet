using Bizomet.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bizomet.Data.Configurations
{
	public class ProjectConfiguration : IEntityTypeConfiguration<Project>
	{
		public void Configure(EntityTypeBuilder<Project> builder)
		{
			// Primary key
			builder.HasKey(s => s.Id);

			builder.Property(s => s.Title).HasMaxLength(1000).IsRequired(true);
			builder.Property(s => s.Description).IsRequired(true);
			builder.Property(s => s.RequestDate)
				.IsRequired(true)
				.HasConversion(
					v => v.ToUniversalTime(),
					v => v.ToLocalTime()
				);
			builder.HasIndex(x => x.RequestDate).IsUnique(false);

			builder.HasOne(s => s.User)
				.WithMany(s => s.Projects)
				.HasForeignKey(s => s.UserId);
		}
	}
}
