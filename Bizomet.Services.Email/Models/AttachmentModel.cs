namespace MailKitMailer.Models
{
	public class AttachmentModel
	{
		public string FilePath { get; set; }

		public Uri FileUrl { get; set; }

		public string ContentType { get; set; }
	}
}
