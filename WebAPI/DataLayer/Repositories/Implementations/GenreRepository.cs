using DataLayer.Data;
using DataLayer.Models;
using DataLayer.Repositories.Contracts;

namespace DataLayer.Repositories.Implementations
{
	public class GenreRepository : BaseRepository<Genre>, IGenreRepository
	{
		public GenreRepository(DataContext context) : base(context) { }

		public bool GenreNameExists(string genreName)
			=> dbSet.Any(genre => genre.Name == genreName);
	}
}
