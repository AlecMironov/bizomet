using System.ComponentModel.DataAnnotations;

namespace Bizomet.Models
{
	public class ResetPasswordModel
	{
		[Required(ErrorMessage = "Password is required")]
		public string Password { get; set; }

		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }

		public string UserName { get; set; }
		public string Token { get; set; }
	}
}
