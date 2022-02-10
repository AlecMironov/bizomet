namespace Bizomet.Models.MailModels
{
	public class BaseModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName => $"{FirstName} {LastName}";
		public string UserName { get; set; }
		public string Email { get; set; }
		public DateTime Date { get; set; }
	}
}
