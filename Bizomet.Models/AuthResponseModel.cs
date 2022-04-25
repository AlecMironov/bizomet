namespace Bizomet.Models
{
	public class AuthResponseModel
	{
		public string id { get; set; }

		public string userName { get; set; }

		public string email { get; set; }

		public string firstName { get; set; }

		public string lastName { get; set; }

		public string publicName { get; set; }

		public string phoneNumber { get; set; }

		public IList<string> roles { get; set; }

		public string token { get; set; }

		public string refreshToken { get; set; }

		public string picture { get; set; }
	}
}
