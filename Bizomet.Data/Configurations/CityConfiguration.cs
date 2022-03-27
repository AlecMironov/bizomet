using Bizomet.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bizomet.Data.Configurations
{
	public class CityConfiguration : IEntityTypeConfiguration<City>
	{
		public void Configure(EntityTypeBuilder<City> builder)
		{
			builder.ToTable("Location.Cities");

			builder.HasKey(s => s.Id);

			builder.Property(s => s.Name).HasMaxLength(510).IsRequired(true);
			builder.Property(s => s.CountryCode).HasMaxLength(10).IsRequired(false);
			builder.Property(s => s.StateCode).HasMaxLength(510).IsRequired(false);
			builder.Property(s => s.Latitude).HasPrecision(11, 8).IsRequired(false);
			builder.Property(s => s.Longitude).HasPrecision(11, 8).IsRequired(false);

			builder.HasIndex(s => new { s.CountryId, s.StateId, s.Id }).HasDatabaseName("CountryStateCityByIdIndex").IsUnique(true);
			builder.HasIndex(s => new { s.CountryCode, s.StateCode, s.Name }).HasDatabaseName("CountryStateCityIndex").IsUnique(false);
			builder.HasIndex(s => new { s.StateCode, s.Name }).HasDatabaseName("StateCityIndex").IsUnique(false);
			builder.HasIndex(s => s.Name).HasDatabaseName("CityIndex").IsUnique(false);

			builder
				.HasOne(s => s.State)
				.WithMany(g => g.Cities)
				.HasForeignKey(s => s.StateId)
				.OnDelete(DeleteBehavior.NoAction);

			builder
				.HasOne(s => s.Country)
				.WithMany(s => s.Cities)
				.HasForeignKey(s => s.CountryId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
