using DataLayer.Data;
using DataLayer.Models;
using DataLayer.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementations
{
	public class ReviewRepository : BaseRepository<Review>, IReviewRepository
	{
		public ReviewRepository(DataContext context) : base(context) { }

		public override Review? GetById(int id)
		{
			return dbSet
				.AsNoTracking()
				.Include(review => review.Movie)
				.Include(review => review.User)
				.FirstOrDefault(review => review.Id == id);
		}

		public bool ReviewExists(int userId, int movieId)
			=> dbSet.Any(review => review.UserId == userId && review.MovieId == movieId);

		public IEnumerable<Review> GetByUser(int userId)
		{
			return dbSet
				.AsNoTracking()
				.Include(review => review.Movie)
				.Include(review => review.User)
				.Where(review => review.UserId == userId);
		}

		public IEnumerable<Review> GetByMovie(int movieId)
		{
			return dbSet
				.AsNoTracking()
				.Include(review => review.Movie)
				.Include(review => review.User)
				.Where(review => review.MovieId == movieId);
		}
	}
}
