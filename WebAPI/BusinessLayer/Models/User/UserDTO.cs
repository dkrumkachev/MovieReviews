using BusinessLayer.Models.Review;

namespace BusinessLayer.Models.User
{
	public class UserDTO
	{
		public int Id { get; set; }

		public string Username { get; set; }

		public ICollection<ReviewDTO> Reviews { get; set; }
	}
}
