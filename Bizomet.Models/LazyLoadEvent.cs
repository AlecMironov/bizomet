namespace Bizomet.Models
{
	public class LazyLoadEvent
	{
		public int? first { get; set; }
		public int? rows { get; set; }
		public string? sortField { get; set; }
		public int? sortOrder { get; set; }
		public List<SortMeta> multiSortMeta { get; set; }
		//public List<FilterMetadata> filters { get; set; }
		public object globalFilter { get; set; }
	}

	public class SortMeta
	{
		public string field { get; set; }
		public int order { get; set; }
	}
}
