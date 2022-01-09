namespace Bizomet.ViewModels
{
	public class RegistrationResponseViewModel
	{
		public bool IsSuccessfulRegistration { get; set; }
		public IEnumerable<string> Errors { get; set; }
	}
}
