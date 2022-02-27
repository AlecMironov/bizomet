using System.ComponentModel.DataAnnotations;
using Bizomet.Core.Enums;

namespace Bizomet.Models
{
	public class ContactUsRequestModel
	{
		public string UserId { get; set; }

		[Required(ErrorMessage = "Email is required")]
		public string Reason { get; set; }

		[Required(ErrorMessage = "First Name is required")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Last Name is required")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress]
		public string Email { get; set; }

		[Phone]
		public string PhoneNumber { get; set; }

		[Required(ErrorMessage = "Message is required")]
		public string Description { get; set; }

		public string Captcha { get; set; }
	}
}
