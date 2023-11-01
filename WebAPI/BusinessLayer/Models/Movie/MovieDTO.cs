using BusinessLayer.Models.Country;
using BusinessLayer.Models.Director;
using BusinessLayer.Models.Genre;
using BusinessLayer.Models.Review;

namespace BusinessLayer.Models.Movie
{
	public class MovieDTO
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public int Year { get; set; }

		public string? Description { get; set; }

		public string? ImagePath { get; set; }

		public int DirectorId { get; set; }

		public DirectorDTO Director { get; set; }
		
		public int CountryID { get; set; }

		public CountryDTO Country { get; set; }

		public double Rating { get; set; }

		public int ReviewsCount { get; set; }

		public ICollection<GenreDTO> Genres { get; set; }

		public ICollection<ReviewDTO> Reviews { get; set; }
	}
}
