using System.ComponentModel.DataAnnotations;

namespace Bizomet.Models
{
	public class UserProfileModel
	{
		public string UserName { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress]
		public string Email { get; set; }

		public IEnumerable<string> Roles { get; set; }

		public string NameTitle { get; set; }

		public string AddressLine1 { get; set; }

		public string AddressLine2 { get; set; }

		public string City { get; set; }

		public string StateProvince { get; set; }

		public string Country { get; set; }

		public string ZipPostal { get; set; }

		public string PhoneNumber { get; set; }

		public string PhoneNumberBusiness { get; set; }

		public string PhoneNumberHome { get; set; }

		public string PhoneNumberCell { get; set; }

		public string PhoneNumberFax { get; set; }

		public string PictureLarge { get; set; }

		public string PictureSmall { get; set; }

		public string LocationCountry { get; set; }

		public string LocationState { get; set; }

		public string LocationCity { get; set; }
	}
}
