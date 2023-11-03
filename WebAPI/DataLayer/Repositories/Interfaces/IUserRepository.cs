using DataLayer.Models;

namespace DataLayer.Repositories.Interfaces
{
	public interface IUserRepository : IBaseRepository<User>
	{
		User? GetByUsername(string username);

		bool UsernameExists(string username);
	}
}
