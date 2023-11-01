using DataLayer.Models;

namespace DataLayer.Repositories.Contracts
{
	public interface ICountryRepository : IBaseRepository<Country>
	{
		bool CountryNameExists(string countryName);
	}
}
