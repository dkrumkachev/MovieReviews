using DataLayer.Models;

namespace DataLayer.Repositories.Contracts
{
	public interface IReviewRepository : IBaseRepository<Review>
	{
		bool ReviewExists(int userId, int movieId);

		IEnumerable<Review> GetByUser(int userId);

		IEnumerable<Review> GetByMovie(int movieId);
	}
}
