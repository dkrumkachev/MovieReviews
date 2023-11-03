using BusinessLayer.Models.User;
using DataLayer.Models;

namespace BusinessLayer.Services.Interfaces
{
	public interface IUserService
	{
		User Create(UserRegisterDTO user);

		UserDTO Update(int userId, UserUpdateUsernameDTO user);

		UserDTO Update(int userId, UserUpdatePasswordDTO user);

		void Delete(int userId);

		UserDTO GetById(int userId);
	}
}
