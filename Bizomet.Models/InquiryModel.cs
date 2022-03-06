using System.ComponentModel.DataAnnotations;

namespace Bizomet.Models
{
	public class InquiryModel
	{
		public string Id { get; set; }

		public DateTime RequestDate { get; set; }

		[Required]
		public string Title { get; set; }

		[Required]
		public string Description { get; set; }
	}
}
