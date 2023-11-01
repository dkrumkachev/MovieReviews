namespace BusinessLayer.Models.Review
{
	public class ReviewCreateDTO
	{
		public string? Text { get; set; }

		public int UserId { get; set; }

		public int MovieId { get; set; }

		public int Rating { get; set; }
	}
}
