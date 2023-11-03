using DataLayer.Repositories.Interfaces;

namespace DataLayer.Repositories.UnitOfWork
{
	public interface IRepositoryManager
	{
		ICountryRepository Countries { get; }

		IDirectorRepository Directors { get; }

		IGenreRepository Genres { get; }

		IMovieRepository Movies { get; }

		IReviewRepository Reviews { get; }

		IUserRepository Users { get; }

		void Save();
	}

}
