using Bizomet.Core.Enums;
using Bizomet.Data.DataEncryption.Attributes;

namespace Bizomet.Data.Entities
{
	public class ContactUsRequest : EntityBase
	{
		public string UserId { get; set; }

		public ContactReason Reason { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string AddressLine1 { get; set; }

		public string AddressLine2 { get; set; }

		public string City { get; set; }

		public string StateProvince { get; set; }

		public string Country { get; set; }

		public string Email { get; set; }

		public string PhoneNumber { get; set; }

		public string Description { get; set; }
	}
}