using System.Text.Json;
using Bizomet.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Bizomet.Data.Configurations
{
	public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
	{
		public void Configure(EntityTypeBuilder<UserProfile> builder)
		{
			// Primary key
			builder.HasKey(u => u.Id);

			// Limit the size of columns to use efficient database types
			builder.Property(u => u.PublicName).IsRequired(true).HasMaxLength(200);
			builder.Property(u => u.FirstName).HasMaxLength(100);
			builder.Property(u => u.LastName).HasMaxLength(100);

			builder.Property(u => u.NameTitle).IsRequired(false).HasMaxLength(10);
			builder.Property(u => u.AddressLine1).IsRequired(false).HasMaxLength(100);
			builder.Property(u => u.AddressLine2).IsRequired(false).HasMaxLength(100);
			builder.Property(u => u.City).IsRequired(false).HasMaxLength(100);
			builder.Property(u => u.StateProvince).IsRequired(false).HasMaxLength(100);
			builder.Property(u => u.Country).IsRequired(false).HasMaxLength(100);
			builder.Property(u => u.ZipPostal).IsRequired(false).HasMaxLength(20);

			builder.Property(u => u.PhoneNumberBusiness).IsRequired(false).HasMaxLength(50);
			builder.Property(u => u.PhoneNumberHome).IsRequired(false).HasMaxLength(50);
			builder.Property(u => u.PhoneNumberCell).IsRequired(false).HasMaxLength(50);
			builder.Property(u => u.PhoneNumberFax).IsRequired(false).HasMaxLength(50);

			builder.Property(u => u.LocationCity).IsRequired(false).HasMaxLength(100);
			builder.Property(u => u.LocationState).IsRequired(false).HasMaxLength(100);
			builder.Property(u => u.LocationCountry).IsRequired(false).HasMaxLength(100);

			builder.Property(u => u.Picture).IsRequired(false);
			builder.Property(u => u.Description).IsRequired(false);

			builder.HasIndex(u => u.PublicName).HasDatabaseName("PublicNameIndex").IsUnique();

			builder.Property(p => p.Tags)
				.IsRequired(false)
				.HasConversion(
					v => JsonConvert.SerializeObject(v),
					v => JsonConvert.DeserializeObject<List<string>>(v),
					new ValueComparer<List<string>>(
						(c1, c2) => c1.SequenceEqual(c2),
						c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
						c => c.ToList()));

			builder.HasOne<ApplicationUser>(e => e.User)
				.WithOne(e => e.UserProfile)
				.HasForeignKey<UserProfile>(e => e.UserId);
		}
	}
}
