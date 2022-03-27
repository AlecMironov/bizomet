namespace Bizomet.Data.Entities
{
	public class Country
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string ISO3 { get; set; }

		public string NumericCode { get; set; }

		public string ISO2 { get; set; }

		public string PhoneCode { get; set; }

		public string Capital { get; set; }

		public string Currency { get; set; }

		public string CurrencyName { get; set; }

		public string CurrencySymbol { get; set; }

		public string TLD { get; set; }

		public string Region { get; set; }

		public string SubRegion { get; set; }

		public decimal? Latitude { get; set; }

		public decimal? Longitude { get; set; }

		public ICollection<State> States { get; set; }

		public ICollection<City> Cities { get; set; }
	}
}
