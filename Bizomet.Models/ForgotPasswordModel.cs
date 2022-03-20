using System.ComponentModel.DataAnnotations;

namespace Bizomet.Models
{
	public class ForgotPasswordModel
	{
		[Required]
		public string UserName { get; set; }

		[Required]
		public string ClientURI { get; set; }
	}
}
