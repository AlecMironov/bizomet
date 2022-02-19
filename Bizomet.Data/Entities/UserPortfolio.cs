namespace Bizomet.Data.Entities
{
	public class UserPortfolio : EntityBase
	{
		public string UserId { get; set; }

		public ApplicationUser User { get; set; }

		public string Title { get; set; }

		public string Link { get; set; }
	}
}