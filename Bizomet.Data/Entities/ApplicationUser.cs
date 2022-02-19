using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Bizomet.Data.Entities
{
	public class ApplicationUser : IdentityUser
	{
		private UserProfile _userProfile;

		private ApplicationUser()
		{
		}

		private ApplicationUser(ILazyLoader lazyLoader)
		{
			LazyLoader = lazyLoader;
		}

		private ILazyLoader LazyLoader { get; set; }

		public virtual UserProfile UserProfile {
			get => LazyLoader.Load(this, ref _userProfile);
			set => _userProfile = value;
		}

		public string RefreshToken { get; set; }

		public DateTime? RefreshTokenExpiryTime { get; set; }

		public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
		public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
		public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
		public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
		public virtual ICollection<UserPortfolio> UserPortfolio { get; set; }
	}
}