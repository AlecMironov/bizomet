using Bizomet.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bizomet.Data.Configurations
{
	public class ContactUsRequestConfiguration : IEntityTypeConfiguration<ContactUsRequest>
	{
		public void Configure(EntityTypeBuilder<ContactUsRequest> builder)
		{
			// Primary key
			builder.HasKey(u => u.Id);

			builder.Property(u => u.UserId).HasMaxLength(450).IsRequired(false);
			builder.HasIndex(u => u.UserId).HasDatabaseName("UserContactUsRequestsIndex").IsUnique(false);

			// Limit the size of columns to use efficient database types
			builder.Property(u => u.FirstName).HasMaxLength(500);
			builder.Property(u => u.LastName).HasMaxLength(500);

			builder.Property(u => u.AddressLine1).IsRequired(false).HasMaxLength(100);
			builder.Property(u => u.AddressLine2).IsRequired(false).HasMaxLength(100);
			builder.Property(u => u.City).IsRequired(false).HasMaxLength(100);
			builder.Property(u => u.StateProvince).IsRequired(false).HasMaxLength(100);
			builder.Property(u => u.Country).IsRequired(false).HasMaxLength(100);

			builder.Property(u => u.PhoneNumber).IsRequired(false).HasMaxLength(50);
			builder.Property(u => u.Email).HasMaxLength(150).IsRequired(true);

			builder.Property(u => u.Description).IsRequired(false);
		}
	}
}
