using AutoMapper;
using BusinessLayer.Exceptions;
using BusinessLayer.Models.Movie;
using BusinessLayer.Services.Interfaces;
using DataLayer.Models;
using DataLayer.Repositories.UnitOfWork;

namespace BusinessLayer.Services.Implementations
{
	public class MovieService : IMovieService
	{
		private readonly IRepositoryManager repositoryManager;
		private readonly IMapper mapper;

		public MovieService(IRepositoryManager repositoryManager, IMapper mapper)
		{
			this.repositoryManager = repositoryManager;
			this.mapper = mapper;
		}

		public MovieDTO Create(MovieCreateDTO movie)
		{
			var newMovie = mapper.Map<Movie>(movie);
			repositoryManager.Movies.Create(newMovie);
			repositoryManager.Save();
			return mapper.Map<MovieDTO>(newMovie);
		}

		public MovieDTO Update(int movieId, MovieUpdateDTO movie)
		{
			var movieToUpdate = repositoryManager.Movies.GetById(movieId)
				?? throw new EntityNotFoundException($"Movie with id {movieId} does not exist.");
			mapper.Map(movie, movieToUpdate);
			repositoryManager.Movies.Update(movieToUpdate);
			repositoryManager.Save();
			return mapper.Map<MovieDTO>(movieToUpdate);
		}

		public void Delete(int movieId)
		{
			var movieToDelete = repositoryManager.Movies.GetById(movieId)
				?? throw new EntityNotFoundException($"Movie with id {movieId} does not exist.");
			repositoryManager.Movies.Delete(movieToDelete);
			repositoryManager.Save();
		}

		public MovieDTO GetById(int movieId)
		{
			var movie = repositoryManager.Movies.GetById(movieId) 
				?? throw new EntityNotFoundException($"Movie with id {movieId} does not exist.");
			return mapper.Map<MovieDTO>(movie);
		}

		public IEnumerable<MovieDTO> GetAll()
		{
			var movies = repositoryManager.Movies.GetAll();
			return mapper.Map<IEnumerable<MovieDTO>>(movies);
		}

		public IEnumerable<MovieDTO> GetByCountry(int countryId)
		{
			var movies = repositoryManager.Movies.GetByCountry(countryId)
				?? throw new EntityNotFoundException($"Country with id {countryId} does not exist.");
			return mapper.Map<IEnumerable<MovieDTO>>(movies);
		}

		public IEnumerable<MovieDTO> GetByDirector(int directorId)
		{
			var movies = repositoryManager.Movies.GetByCountry(directorId)
				?? throw new EntityNotFoundException($"Director with id {directorId} does not exist.");
			return mapper.Map<IEnumerable<MovieDTO>>(movies);
		}

		public IEnumerable<MovieDTO> GetByGenre(int genreId)
		{
			var movies = repositoryManager.Movies.GetByCountry(genreId)
				?? throw new EntityNotFoundException($"Genre with id {genreId} does not exist.");
			return mapper.Map<IEnumerable<MovieDTO>>(movies);
		}
	}
}
