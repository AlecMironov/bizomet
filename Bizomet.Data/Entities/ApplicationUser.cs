using Bizomet.Data.DataEncryption.Attributes;
using Microsoft.AspNetCore.Identity;

namespace Bizomet.Data.Entities
{
	public class ApplicationUser : IdentityUser
	{
		[Encrypted]
		public string FirstName { get; set; }

		[Encrypted]
		public string LastName { get; set; }

		[Encrypted]
		public string NameTitle { get; set; }

		[Encrypted]
		public string AddressLine1 { get; set; }

		[Encrypted]
		public string AddressLine2 { get; set; }

		[Encrypted]
		public string City { get; set; }

		[Encrypted]
		public string Province { get; set; }

		[Encrypted]
		public string Country { get; set; }

		[Encrypted]
		public string PostalCode { get; set; }

		[Encrypted]
		public string PhoneNumberBusiness { get; set; }

		[Encrypted]
		public string PhoneNumberHome { get; set; }

		[Encrypted]
		public string PhoneNumberCell { get; set; }

		[Encrypted]
		public string PhoneNumberFax { get; set; }

		public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
		public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
		public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
		public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
	}
}