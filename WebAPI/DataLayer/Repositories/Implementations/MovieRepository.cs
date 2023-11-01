using Microsoft.EntityFrameworkCore;
using DataLayer.Models;
using DataLayer.Data;
using DataLayer.Repositories.Contracts;

namespace DataLayer.Repositories.Implementations
{
	public class MovieRepository : BaseRepository<Movie>, IMovieRepository
	{
		public MovieRepository(DataContext context) : base(context) { }
		
		public override Movie? GetById(int id)
		{
			return dbSet
				.AsNoTracking()
				.Include(movie => movie.Country)
				.Include(movie => movie.Director)
				.Include(movie => movie.Genres)
				.Include(movie => movie.Reviews)
				.ThenInclude(review => review.User)
				.FirstOrDefault(movie => movie.Id == id);
		}

		public override IEnumerable<Movie> GetAll()
			=> dbSet.AsNoTracking();

		public IEnumerable<Movie> GetByCountry(int countryId)
		{
			return dbSet.AsNoTracking().Where(movie => movie.CountryId == countryId);
		}

		public IEnumerable<Movie> GetByDirector(int directorId)
		{
			return dbSet.AsNoTracking().Where(movie => movie.DirectorId == directorId);
		}

		public IEnumerable<Movie> GetByGenre(int genreId)
		{
			return dbSet
				.AsNoTracking()
				.Include(movie => movie.Genres)
				.Where(movie => movie.Genres.Any(genre => genre.Id == genreId));
		}

		//public IEnumerable<Movie> GetByParameters(MovieParameters movieParams)
		//{
		//	return dbSet
		//		.Include(movie => movie.Reviews)
		//		.Include(movie => movie.Genres)
		//		.Where(movie => movie.Year >= movieParams.MinYear && movie.Year <= movieParams.MaxYear)
		//		.Search(movieParams.SearchTerm)
		//		.Sort(movieParams.OrderBy);
		//}	
	}
}
