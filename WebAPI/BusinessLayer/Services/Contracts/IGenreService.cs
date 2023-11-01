using BusinessLayer.Models.Genre;

namespace BusinessLayer.Services.Contracts
{
	public interface IGenreService
	{
		GenreDTO Create(GenreCreateDTO genre);

		GenreDTO Update(int genreId, GenreUpdateDTO genre);

		void Delete(int genreId);

		GenreDTO GetById(int genreId);

		IEnumerable<GenreDTO> GetAll();
	}
}
