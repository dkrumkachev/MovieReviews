namespace DataLayer.RequestFeatures
{
	public class MovieParameters : RequestParameters
	{
		public uint MinYear { get; set; }

		public uint MaxYear { get; set; } = int.MaxValue;

		public bool ValidYearRange => MaxYear >= MinYear;

		public string SearchTerm { get; set; }
	}
}
