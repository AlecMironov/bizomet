namespace Bizomet.Models
{
	public class ProjectAttachmentModel
	{
		public string Id { get; set; }

		public string ProjectId { get; set; }

		public string FileName { get; set; }

		public string Title { get; set; }

		public long Size { get; set; }

		public string FileType { get; set; }

		public string BinaryContent { get; set; }

		public string Thumbnail { get; set; }
	}
}
