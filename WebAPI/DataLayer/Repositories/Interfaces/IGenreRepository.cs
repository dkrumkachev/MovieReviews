using DataLayer.Models;

namespace DataLayer.Repositories.Interfaces
{
	public interface IGenreRepository : IBaseRepository<Genre>
	{
		bool GenreNameExists(string genreName);
	}
}
