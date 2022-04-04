using Bizomet.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bizomet.Data.Configurations
{
	public class ProjectAttachmentConfiguration : IEntityTypeConfiguration<ProjectAttachment>
	{
		public void Configure(EntityTypeBuilder<ProjectAttachment> builder)
		{
			// Primary key
			builder.HasKey(s => s.Id);

			builder.Property(s => s.FileName).HasMaxLength(2000).IsRequired(true);
			builder.Property(s => s.Title).HasMaxLength(500).IsRequired(false);
			builder.Property(s => s.BinaryContent).IsRequired(true);
			builder.Property(s => s.Thumbnail).IsRequired(false);
			builder.Property(s => s.FileType).HasMaxLength(100).IsRequired(true);

			builder.HasOne(s => s.Project)
				.WithMany(s => s.ProjectAttachments)
				.HasForeignKey(s => s.ProjectId);
		}
	}
}
