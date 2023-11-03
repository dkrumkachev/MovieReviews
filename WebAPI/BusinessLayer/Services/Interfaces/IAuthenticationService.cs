using BusinessLayer.Models.User;

namespace BusinessLayer.Services.Interfaces
{
	public interface IAuthenticationService
	{
		AuthenticatedUserDTO Register(UserRegisterDTO user);

		AuthenticatedUserDTO LogIn(UserLogInDTO user);
	}
}
