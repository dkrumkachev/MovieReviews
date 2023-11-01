using DataLayer.Models;

namespace DataLayer.Repositories.Contracts
{
	public interface IUserRepository : IBaseRepository<User>
	{
		User? GetByUsername(string username);

		bool UsernameExists(string username);
	}
}
