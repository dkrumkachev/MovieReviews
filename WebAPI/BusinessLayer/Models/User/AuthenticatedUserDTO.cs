namespace BusinessLayer.Models.User
{
	public class AuthenticatedUserDTO
	{
		public int Id { get; set; }

		public string Username { get; set; }

		public string JwtToken { get; set; }

		public string Role { get; set; }
	}
}
