using BusinessLayer.Models.Movie;
using BusinessLayer.Models.User;

namespace BusinessLayer.Models.Review
{
	public class ReviewDTO
	{
		public int Id { get; set; }

		public int UserId { get; set; }

		public UserDTO User { get; set; }

		public int MovieId { get; set; }

		public MovieDTO Movie { get; set; }

		public string Text { get; set; }

		public int Rating { get; set; }
	}
}
