using System.ComponentModel.DataAnnotations;

namespace Bizomet.Models
{
	public class UserAuthenticationModel
	{
		[Required(ErrorMessage = "Username is required")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		public string clientURI { get; set; }
	}
}
