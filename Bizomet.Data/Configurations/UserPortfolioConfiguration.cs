using Bizomet.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bizomet.Data.Configurations
{
	public class UserPortfolioConfiguration : IEntityTypeConfiguration<UserPortfolio>
	{
		public void Configure(EntityTypeBuilder<UserPortfolio> builder)
		{
			// Primary key
			builder.HasKey(s => s.Id);

			builder.Property(s => s.Title).HasMaxLength(500).IsRequired(true);
			builder.Property(s => s.Link).IsRequired(true);

			builder.HasOne<ApplicationUser>(s => s.User)
				.WithMany(s => s.UserPortfolio)
				.HasForeignKey(s => s.UserId);
		}
	}
}
