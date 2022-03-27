using Bizomet.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
			builder.Property(s => s.InterviewConditionComment).IsRequired(false).HasMaxLength(4000);
			builder.Property(s => s.InterviewResultComment).IsRequired(false).HasMaxLength(4000);
			builder.Property(s => s.Location).IsRequired(false).HasMaxLength(1000);

			builder.Property(s => s.DueDate)
				.HasConversion(
					v => v.ToUniversalTime(),
					v => v.ToLocalTime()
				);

			builder.Property(s => s.RequestDate)
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
