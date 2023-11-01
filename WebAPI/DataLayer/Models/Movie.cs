namespace DataLayer.Models
{
	public class Movie : BaseModel
	{
		public string Title { get; set; }

		public int Year { get; set; }

		public string? Description { get; set; }

		public string? ImagePath { get; set; }

		public int DirectorId { get; set; }
		
		public Director Director { get; set; }

		public int CountryId { get; set; }
		
		public Country Country { get; set; }

        public double Rating { get; set; }

        public int ReviewsCount { get; set; }

        public ICollection<Review> Reviews { get; set; }

		public ICollection<Genre> Genres { get; set; }
	}
}
