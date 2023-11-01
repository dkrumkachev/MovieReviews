using DataLayer.Models;

namespace DataLayer.Repositories.Contracts
{
	public interface IMovieRepository : IBaseRepository<Movie>
	{
		IEnumerable<Movie> GetByCountry(int countryId);

		IEnumerable<Movie> GetByDirector(int directorId);

		IEnumerable<Movie> GetByGenre(int genreId);

		//IEnumerable<Movie> GetByParameters(MovieParameters movieParams);
	}
}
