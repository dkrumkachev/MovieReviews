using DataLayer.Repositories.Contracts;
using DataLayer.Data;
using DataLayer.Repositories.Implementations;

namespace DataLayer.Repositories.UnitOfWork
{
	public class RepositoryManager : IRepositoryManager
    {
        private readonly DataContext context;
        private ICountryRepository? countryRepository;
        private IDirectorRepository? directorRepository;
        private IGenreRepository? genreRepository;
        private IMovieRepository? movieRepository;
        private IReviewRepository? reviewRepository;
        private IUserRepository? userRepository;

        public RepositoryManager(DataContext context)
        {
            this.context = context;
        }

        public ICountryRepository Countries
            => countryRepository ??= new CountryRepository(context);

		public IDirectorRepository Directors
            => directorRepository ??= new DirectorRepository(context);

		public IGenreRepository Genres
			=> genreRepository ??= new GenreRepository(context);

		public IMovieRepository Movies
            => movieRepository ??= new MovieRepository(context);

		public IReviewRepository Reviews
            => reviewRepository ??= new ReviewRepository(context);

		public IUserRepository Users
            => userRepository ??= new UserRepository(context);

		public void Save()
            => context.SaveChanges();
    }
}
