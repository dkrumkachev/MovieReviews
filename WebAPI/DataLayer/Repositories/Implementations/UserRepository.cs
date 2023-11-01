using DataLayer.Data;
using DataLayer.Models;
using DataLayer.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementations
{
	public class UserRepository : BaseRepository<User>, IUserRepository
	{
		public UserRepository(DataContext context) : base(context) { }

		public override User? GetById(int id)
		{
			return dbSet
				.AsNoTracking()
				.Include(user => user.Reviews)
				.ThenInclude(review => review.Movie)
				.FirstOrDefault(user => user.Id == id);
		}

		public User? GetByUsername(string username)
		{
			return dbSet
				.AsNoTracking()
				.Include(user => user.Reviews)
				.ThenInclude(review => review.Movie)
				.FirstOrDefault(user => user.Username == username);
		}

		public bool UsernameExists(string username)
			=> dbSet.Any(user => user.Username == username);
	}
}
