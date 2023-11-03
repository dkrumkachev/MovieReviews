using BusinessLayer.Models.Movie;

namespace BusinessLayer.Services.Interfaces
{
	public interface IMovieService
	{
		MovieDTO Create(MovieCreateDTO movie);

		MovieDTO Update(int movieId, MovieUpdateDTO movie);

		void Delete(int movieId);

		MovieDTO GetById(int movieId);

		IEnumerable<MovieDTO> GetAll();

		IEnumerable<MovieDTO> GetByCountry(int countryId);

		IEnumerable<MovieDTO> GetByDirector(int directorId);

		IEnumerable<MovieDTO> GetByGenre(int genreId);

		//IEnumerable<MovieDTO> GetByParameters(MovieParameters movieParams);
	}
}
