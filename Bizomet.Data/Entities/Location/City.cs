namespace Bizomet.Data.Entities
{
	public class City
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public decimal? Latitude { get; set; }

		public decimal? Longitude { get; set; }

		public string CountryCode { get; set; }

		public int CountryId { get; set; }

		public Country Country { get; set; }

		public string StateCode { get; set; }

		public int StateId { get; set; }

		public State State { get; set; }
	}
}
