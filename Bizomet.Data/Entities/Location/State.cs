namespace Bizomet.Data.Entities
{
	public class State
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string FipsCode { get; set; }

		public string ISO2 { get; set; }

		public string Type { get; set; }

		public decimal? Latitude { get; set; }

		public decimal? Longitude { get; set; }

		public string CountryCode { get; set; }

		public int CountryId { get; set; }

		public Country Country { get; set; }

		public ICollection<City> Cities { get; set; }
	}
}
