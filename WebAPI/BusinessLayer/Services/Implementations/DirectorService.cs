using AutoMapper;
using BusinessLayer.Exceptions;
using BusinessLayer.Models.Director;
using BusinessLayer.Services.Interfaces;
using DataLayer.Models;
using DataLayer.Repositories.UnitOfWork;

namespace BusinessLayer.Services.Implementations
{
	public class DirectorService : IDirectorService
	{
		private readonly IRepositoryManager repositoryManager;
		private readonly IMapper mapper;

		public DirectorService(IRepositoryManager repositoryManager, IMapper mapper)
		{
			this.repositoryManager = repositoryManager;
			this.mapper = mapper;
		}

		public DirectorDTO Create(DirectorCreateDTO director)
		{
			var newDirector = mapper.Map<Director>(director);
			repositoryManager.Directors.Create(newDirector);
			repositoryManager.Save();
			return mapper.Map<DirectorDTO>(newDirector);
		}

		public DirectorDTO Update(int directorId, DirectorUpdateDTO director)
		{
			var directorToUpdate = repositoryManager.Directors.GetById(directorId)
				?? throw new EntityNotFoundException($"Director with id {directorId} does not exist.");
			mapper.Map(director, directorToUpdate);
			repositoryManager.Directors.Update(directorToUpdate);
			repositoryManager.Save();
			return mapper.Map<DirectorDTO>(directorToUpdate);
		}

		public void Delete(int directorId)
		{
			var directorToDelete = repositoryManager.Directors.GetById(directorId)
				?? throw new EntityNotFoundException($"Director with id {directorId} does not exist.");
			repositoryManager.Directors.Delete(directorToDelete);
			repositoryManager.Save();
		}

		public DirectorDTO GetById(int directorId)
		{
			var director = repositoryManager.Directors.GetById(directorId)
				?? throw new EntityNotFoundException($"Director with id {directorId} does not exist.");
			return mapper.Map<DirectorDTO>(director);
		}

		public IEnumerable<DirectorDTO> GetAll()
		{
			var directors = repositoryManager.Directors.GetAll();
			return mapper.Map<IEnumerable<DirectorDTO>>(directors);
		}
	}
}
