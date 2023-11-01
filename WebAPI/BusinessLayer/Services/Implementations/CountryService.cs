using AutoMapper;
using BusinessLayer.Exceptions;
using BusinessLayer.Models.Country;
using BusinessLayer.Services.Contracts;
using DataLayer.Models;
using DataLayer.Repositories.UnitOfWork;

namespace BusinessLayer.Services.Implementations
{
	public class CountryService : ICountryService
	{
		private readonly IRepositoryManager repositoryManager;
		private readonly IMapper mapper;

		public CountryService(IRepositoryManager repositoryManager, IMapper mapper)
		{
			this.repositoryManager = repositoryManager;
			this.mapper = mapper;
		}

		public CountryDTO Create(CountryCreateDTO country)
		{
			if (repositoryManager.Countries.CountryNameExists(country.Name))
			{
				throw new EntityAlreadyExistsException($"Country named '{country.Name}' already exists.");
			}
			var newCountry = mapper.Map<Country>(country);
			repositoryManager.Countries.Create(newCountry);
			repositoryManager.Save();
			return mapper.Map<CountryDTO>(country);
		}

		public CountryDTO Update(int countryId, CountryUpdateDTO country)
		{
			var countryToUpdate = repositoryManager.Countries.GetById(countryId)
				?? throw new EntityNotFoundException($"Country with id {countryId} does not exist.");
			if (country.Name != countryToUpdate.Name && repositoryManager.Countries.CountryNameExists(country.Name))
			{
				throw new EntityAlreadyExistsException($"Country named '{country.Name}' already exists.");
			}
			mapper.Map(country, countryToUpdate);
			repositoryManager.Countries.Update(countryToUpdate);
			repositoryManager.Save();
			return mapper.Map<CountryDTO>(countryToUpdate);
		}

		public void Delete(int countryId)
		{
			var countryToDelete = repositoryManager.Countries.GetById(countryId)
				?? throw new EntityNotFoundException($"Country with id {countryId} does not exist.");
			repositoryManager.Countries.Delete(countryToDelete);
			repositoryManager.Save();
		}

		public CountryDTO GetById(int countryId)
		{
			var country = repositoryManager.Countries.GetById(countryId)
				?? throw new EntityNotFoundException($"Country with id {countryId} does not exist.");
			return mapper.Map<CountryDTO>(country);
		}

		public IEnumerable<CountryDTO> GetAll()
		{
			var countries = repositoryManager.Countries.GetAll();
			return mapper.Map<IEnumerable<CountryDTO>>(countries);
		}
	}
}
