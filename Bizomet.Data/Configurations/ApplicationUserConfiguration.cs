using Bizomet.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bizomet.Data.Configurations
{
	public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			// Primary key
			builder.HasKey(u => u.Id);

			// Indexes for "normalized" username and email, to allow efficient lookups
			builder.HasIndex(u => u.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique();
			builder.HasIndex(u => u.NormalizedEmail).HasDatabaseName("EmailIndex");

			// A concurrency token for use with the optimistic concurrency checking
			builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

			// Limit the size of columns to use efficient database types
			builder.Property(u => u.UserName).HasMaxLength(500);
			builder.Property(u => u.NormalizedUserName).HasMaxLength(500);
			builder.Property(u => u.FirstName).HasMaxLength(500);
			builder.Property(u => u.LastName).HasMaxLength(500);
			builder.Property(u => u.Email).HasMaxLength(500);
			builder.Property(u => u.NormalizedEmail).HasMaxLength(500);

			builder.Property(u => u.NameTitle).HasMaxLength(10);
			builder.Property(u => u.AddressLine1).HasMaxLength(100);
			builder.Property(u => u.AddressLine2).HasMaxLength(100);
			builder.Property(u => u.City).HasMaxLength(100);
			builder.Property(u => u.Province).HasMaxLength(100);
			builder.Property(u => u.Country).HasMaxLength(100);
			builder.Property(u => u.PostalCode).HasMaxLength(20);

			builder.Property(u => u.PhoneNumber).HasMaxLength(50);
			builder.Property(u => u.PhoneNumberBusiness).HasMaxLength(50);
			builder.Property(u => u.PhoneNumberHome).HasMaxLength(50);
			builder.Property(u => u.PhoneNumberCell).HasMaxLength(50);
			builder.Property(u => u.PhoneNumberFax).HasMaxLength(50);

			// Each User can have many UserClaims
			builder.HasMany(e => e.Claims)
				.WithOne()
				.HasForeignKey(uc => uc.UserId)
				.IsRequired();

			// Each User can have many UserLogins
			builder.HasMany(e => e.Logins)
				.WithOne()
				.HasForeignKey(ul => ul.UserId)
				.IsRequired();

			// Each User can have many UserTokens
			builder.HasMany(e => e.Tokens)
				.WithOne()
				.HasForeignKey(ut => ut.UserId)
				.IsRequired();

			// Each User can have many entries in the UserRole join table
			builder.HasMany(e => e.UserRoles)
				.WithOne(e => e.User)
				.HasForeignKey(ur => ur.UserId)
				.IsRequired();

			//builder.HasData(new ApplicationUser
			//{
			//	Id = Guid.Parse(Constants.DEFAULT_USER_ID),
			//	RMClientId = Guid.Parse(Constants.DEFAULT_RMCLIENT_ID),
			//	UserName = Constants.DEFAULT_ADMIN_USERNAME,
			//	NormalizedUserName = Constants.DEFAULT_ADMIN_USERNAME.ToUpper(),
			//	FirstName = Constants.DEFAULT_ADMIN_FIRSTNAME,
			//	LastName = Constants.DEFAULT_ADMIN_LASTNAME,
			//	Email = Constants.DEFAULT_ADMIN_EMAIL,
			//	NormalizedEmail = Constants.DEFAULT_ADMIN_EMAIL.ToUpper(),
			//	IsActive = true,
			//	EmailConfirmed = true,
			//	SecurityStamp = Guid.NewGuid().ToString(),
			//	PasswordHash = Constants.DEFAULT_ADMIN_PASSWORD_HASH
			//});

			//builder.HasData(new ApplicationUser
			//{
			//	Id = Guid.Parse(Constants.SYSTEM_USER_ID),
			//	RMClientId = Guid.Parse(Constants.SYSTEM_RMCLIENT_ID),
			//	UserName = Constants.SYSTEM_ADMIN_USERNAME,
			//	NormalizedUserName = Constants.SYSTEM_ADMIN_USERNAME.ToUpper(),
			//	FirstName = Constants.SYSTEM_ADMIN_FIRSTNAME,
			//	LastName = Constants.SYSTEM_ADMIN_LASTNAME,
			//	Email = Constants.SYSTEM_ADMIN_EMAIL,
			//	NormalizedEmail = Constants.SYSTEM_ADMIN_EMAIL.ToUpper(),
			//	IsActive = true,
			//	EmailConfirmed = true,
			//	SecurityStamp = Guid.NewGuid().ToString(),
			//	PasswordHash = Constants.SYSTEM_ADMIN_PASSWORD_HASH
			//});
		}
	}
}
