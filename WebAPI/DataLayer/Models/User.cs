namespace DataLayer.Models
{
	public class User : BaseModel
	{
		public string Username { get; set; }

		public string Password { get; set; }

		public bool IsAdmin { get; set; }

		public ICollection<Review> Reviews { get; set; }
	}
}
