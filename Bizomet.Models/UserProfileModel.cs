using System.ComponentModel.DataAnnotations;

namespace Bizomet.Models
{
	public class UserProfileModel
	{
		public string UserUserName { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string PublicName { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress]
		public string UserEmail { get; set; }

		public IEnumerable<string> Roles { get; set; }

		public string NameTitle { get; set; }

		public string AddressLine1 { get; set; }

		public string AddressLine2 { get; set; }

		public string City { get; set; }

		public string StateProvince { get; set; }

		public string Country { get; set; }

		public string ZipPostal { get; set; }

		public string UserPhoneNumber { get; set; }

		public string PhoneNumberBusiness { get; set; }

		public string PhoneNumberHome { get; set; }

		public string PhoneNumberCell { get; set; }

		public string PhoneNumberFax { get; set; }

		public string Picture { get; set; }

		public string LocationCountry { get; set; }

		public string LocationState { get; set; }

		public string LocationCity { get; set; }

		public string Description { get; set; }
		public List<string> Tags { get; set; }
	}
}
