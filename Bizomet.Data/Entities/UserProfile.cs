using Bizomet.Data.DataEncryption.Attributes;

namespace Bizomet.Data.Entities
{
	public class UserProfile : EntityBase
	{
		public string UserId { get; set; }

		public ApplicationUser User { get; set; }

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
		public string StateProvince { get; set; }

		[Encrypted]
		public string Country { get; set; }

		[Encrypted]
		public string ZipPostal { get; set; }

		[Encrypted]
		public string PhoneNumberBusiness { get; set; }

		[Encrypted]
		public string PhoneNumberHome { get; set; }

		[Encrypted]
		public string PhoneNumberCell { get; set; }

		[Encrypted]
		public string PhoneNumberFax { get; set; }

		public string Picture { get; set; }

		public string LocationCountry { get; set; }

		public string LocationState { get; set; }

		public string LocationCity { get; set; }

		public string Description { get; set; }

		public List<string> Tags { get; set; }
	}
}