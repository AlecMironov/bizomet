using System.ComponentModel.DataAnnotations;

namespace Bizomet.Models
{
	public class UserRegistrationModel
	{
		[Required(ErrorMessage = "User Name is required")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "First Name is required")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Last Name is required")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress]
		public string Email { get; set; }

		[Required(ErrorMessage = "Role is required")]
		public string Role { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }

		public string ClientURI { get; set; }

		public string Captcha { get; set; }
	}
}
