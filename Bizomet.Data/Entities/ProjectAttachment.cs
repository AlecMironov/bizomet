namespace Bizomet.Data.Entities
{
	public class ProjectAttachment : EntityBase
	{
		public Guid ProjectId { get; set; }

		public Project Project { get; set; }

		public string FileName { get; set; }

		public string Title { get; set; }

		public long Size { get; set; }

		public string FileType { get; set; }

		public byte[] BinaryContent { get; set; }

		public string Thumbnail { get; set; }
	}
}