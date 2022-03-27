using Bizomet.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bizomet.Data.Configurations
{
	public class CountryConfiguration : IEntityTypeConfiguration<Country>
	{
		public void Configure(EntityTypeBuilder<Country> builder)
		{
			builder.ToTable("Location.Countries");

			builder.HasKey(s => s.Id);

			builder.Property(s => s.Name).HasMaxLength(200).IsRequired(true);
			builder.Property(s => s.ISO3).HasMaxLength(10).IsRequired(true);
			builder.Property(s => s.NumericCode).HasMaxLength(10).IsRequired(false);
			builder.Property(s => s.ISO2).HasMaxLength(10).IsRequired(false);
			builder.Property(s => s.PhoneCode).HasMaxLength(510).IsRequired(false);
			builder.Property(s => s.Capital).HasMaxLength(510).IsRequired(false);
			builder.Property(s => s.Currency).HasMaxLength(510).IsRequired(false);
			builder.Property(s => s.CurrencyName).HasMaxLength(510).IsRequired(false);
			builder.Property(s => s.CurrencySymbol).HasMaxLength(510).IsRequired(false);
			builder.Property(s => s.TLD).HasMaxLength(510).IsRequired(false);
			builder.Property(s => s.Region).HasMaxLength(510).IsRequired(false);
			builder.Property(s => s.SubRegion).HasMaxLength(510).IsRequired(false);
			builder.Property(s => s.Latitude).HasPrecision(11, 8).IsRequired(false);
			builder.Property(s => s.Longitude).HasPrecision(11, 8).IsRequired(false);

			builder.HasIndex(s => s.Name).HasDatabaseName("CountryIndex").IsUnique(false);
		}
	}
}
