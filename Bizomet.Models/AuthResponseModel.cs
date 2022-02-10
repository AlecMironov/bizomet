namespace Bizomet.Models
{
	public class AuthResponseModel
	{
		public string errorMessage { get; set; }
		
		public string id { get; set; }

		public string userName { get; set; }

		public string firstName { get; set; }

		public string lastName { get; set; }

		public IList<string> roles { get; set; }

		public string token { get; set; }

		public string refreshToken { get; set; }
	}
}
