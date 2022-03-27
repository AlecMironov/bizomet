using Bizomet.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bizomet.Data.Configurations
{
	public class StateConfiguration : IEntityTypeConfiguration<State>
	{
		public void Configure(EntityTypeBuilder<State> builder)
		{
			builder.ToTable("Location.States");

			builder.HasKey(s => s.Id);

			builder.Property(s => s.Name).HasMaxLength(510).IsRequired(true);
			builder.Property(s => s.ISO2).HasMaxLength(10).IsRequired(false);
			builder.Property(s => s.Type).HasMaxLength(400).IsRequired(false);
			builder.Property(s => s.CountryCode).HasMaxLength(10).IsRequired(false);
			builder.Property(s => s.FipsCode).HasMaxLength(510).IsRequired(false);
			builder.Property(s => s.Latitude).HasPrecision(11, 8).IsRequired(false);
			builder.Property(s => s.Longitude).HasPrecision(11, 8).IsRequired(false);

			builder.HasIndex(s => new { s.CountryCode, s.Name }).HasDatabaseName("CountryStateIndex").IsUnique(false);
			builder.HasIndex(s => s.Name).HasDatabaseName("StateIndex").IsUnique(false);

			builder
				.HasOne(s => s.Country)
				.WithMany(s => s.States)
				.HasForeignKey(s => s.CountryId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
