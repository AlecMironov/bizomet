namespace Bizomet.Web.Helpers
{
	public class JwtSettings
	{
		public bool ValidateIssuerSigningKey { get; set; }
		public string IssuerSigningKey { get; set; }
		public bool ValidateIssuer { get; set; } = true;
		public string ValidIssuer { get; set; }
		public bool ValidateAudience { get; set; } = true;
		public string ValidAudience { get; set; }
		public bool RequireExpirationTime { get; set; }
		public bool ValidateLifetime { get; set; } = true;
		public int ExpiryInMinutes { get; set; } = 5;

		// refresh token time to live (in days), inactive tokens are
		// automatically deleted from the database after this time
		public int RefreshTokenTTL { get; set; } = 2;
	}
}
