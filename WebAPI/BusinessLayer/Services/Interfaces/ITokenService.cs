using BusinessLayer.Models.User;

namespace BusinessLayer.Services.Interfaces
{
	public interface ITokenService
	{
		string CreateToken(AuthenticatedUserDTO user);
	}
}
