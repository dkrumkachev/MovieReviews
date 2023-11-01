namespace DataLayer.Models
{
	public class Review : BaseModel
	{
		public int UserId { get; set; }
		
		public User User { get; set; }

		public int MovieId { get; set; }
	
		public Movie Movie { get; set; }

		public string? Text { get; set; }

		public int Rating { get; set; }
	}
}
