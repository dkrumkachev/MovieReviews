using BusinessLayer.Models.User;

namespace BusinessLayer.Services.Contracts
{
	public interface ITokenService
	{
		string CreateToken(AuthenticatedUserDTO user);
	}
}
