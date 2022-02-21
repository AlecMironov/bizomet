using System.ComponentModel.DataAnnotations;

namespace Bizomet.Models
{
	public class UserPortfolioModel
	{
		public string Id { get; set; }

		public int Order { get; set; }

		[Required]
		public string Title { get; set; }

		[Required]
		public string Link { get; set; }
	}
}
