using BusinessLayer.Models.User;

namespace BusinessLayer.Services.Contracts
{
	public interface IAuthenticationService
	{
		AuthenticatedUserDTO Register(UserRegisterDTO user);

		AuthenticatedUserDTO LogIn(UserLogInDTO user);
	}
}
