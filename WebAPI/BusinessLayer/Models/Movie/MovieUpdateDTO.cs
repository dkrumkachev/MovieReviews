namespace BusinessLayer.Models.Movie
{
	public class MovieUpdateDTO
	{
		public string Title { get; set; }

		public int Year { get; set; }

		public string? Description { get; set; }

		public string? ImagePath { get; set; }

		public int DirectorId { get; set; }

		public int CountryId { get; set; }
	}
}
