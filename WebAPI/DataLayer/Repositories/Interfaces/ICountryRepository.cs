using DataLayer.Models;

namespace DataLayer.Repositories.Interfaces
{
	public interface ICountryRepository : IBaseRepository<Country>
	{
		bool CountryNameExists(string countryName);
	}
}
