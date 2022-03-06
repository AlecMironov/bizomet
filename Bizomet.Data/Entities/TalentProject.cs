namespace Bizomet.Data.Entities
{
	public class TalentProject //: EntityBase
	{
		public string UserId { get; set; }

		public ApplicationUser User { get; set; }

		public DateTime RequestDate { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }
	}
}