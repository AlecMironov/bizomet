using Bizomet.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bizomet.Data.Configurations
{
	public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
	{
		public void Configure(EntityTypeBuilder<RefreshToken> builder)
		{
			// Primary key
			builder.HasKey(s => s.Id);

			builder.Property(s => s.Token).IsRequired(true);
			builder.Property(s => s.Expires).IsRequired(true);
			builder.Property(s => s.Created).IsRequired(true);
			builder.Property(s => s.CreatedByIp).IsRequired(true);
			builder.Property(s => s.Revoked).IsRequired(false);
			builder.Property(s => s.RevokedByIp).IsRequired(false);
			builder.Property(s => s.ReplacedByToken).IsRequired(false);

			builder.HasOne(s => s.User)
				.WithMany(s => s.RefreshTokens)
				.HasForeignKey(s => s.UserId);
		}
	}
}
