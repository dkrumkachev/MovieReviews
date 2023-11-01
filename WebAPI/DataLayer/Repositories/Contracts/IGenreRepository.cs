using DataLayer.Models;

namespace DataLayer.Repositories.Contracts
{
	public interface IGenreRepository : IBaseRepository<Genre>
	{
		bool GenreNameExists(string genreName);
	}
}
