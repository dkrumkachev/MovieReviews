using AutoMapper;
using BusinessLayer.Exceptions;
using BusinessLayer.Models.Genre;
using BusinessLayer.Services.Interfaces;
using DataLayer.Models;
using DataLayer.Repositories.UnitOfWork;

namespace BusinessLayer.Services.Implementations
{
	public class GenreService : IGenreService
	{
		private readonly IRepositoryManager repositoryManager;
		private readonly IMapper mapper;

		public GenreService(IRepositoryManager repositoryManager, IMapper mapper)
		{
			this.repositoryManager = repositoryManager;
			this.mapper = mapper;
		}

		public GenreDTO Create(GenreCreateDTO genre)
		{
			if (repositoryManager.Genres.GenreNameExists(genre.Name))
			{
				throw new EntityAlreadyExistsException($"Genre named '{genre.Name}' already exists.");
			}
			var newGenre = mapper.Map<Genre>(genre);
			repositoryManager.Genres.Create(newGenre);
			repositoryManager.Save();
			return mapper.Map<GenreDTO>(newGenre);
		}

		public GenreDTO Update(int genreId, GenreUpdateDTO genre)
		{
			var genreToUpdate = repositoryManager.Genres.GetById(genreId)
				?? throw new EntityNotFoundException($"Genre with id {genreId} does not exist.");
			if (genre.Name != genreToUpdate.Name && repositoryManager.Genres.GenreNameExists(genre.Name))
			{
				throw new EntityAlreadyExistsException($"Genre named '{genre.Name}' already exists.");
			}
			mapper.Map(genre, genreToUpdate);
			repositoryManager.Genres.Update(genreToUpdate);
			repositoryManager.Save();
			return mapper.Map<GenreDTO>(genreToUpdate);
		}

		public void Delete(int genreId)
		{
			var genreToDelete = repositoryManager.Genres.GetById(genreId)
				?? throw new EntityNotFoundException($"Genre with id {genreId} does not exist.");
			repositoryManager.Genres.Delete(genreToDelete);
			repositoryManager.Save();
		}

		public GenreDTO GetById(int genreId)
		{
			var genre = repositoryManager.Genres.GetById(genreId)
				?? throw new EntityNotFoundException($"Genre with id {genreId} does not exist.");
			return mapper.Map<GenreDTO>(genre);
		}

		public IEnumerable<GenreDTO> GetAll()
		{
			var genres = repositoryManager.Genres.GetAll();
			return mapper.Map<IEnumerable<GenreDTO>>(genres);
		}
	}
}
