using DataLayer.Data;
using DataLayer.Models;
using DataLayer.Repositories.Interfaces;

namespace DataLayer.Repositories.Implementations
{
	public class CountryRepository : BaseRepository<Country>, ICountryRepository
	{
		public CountryRepository(DataContext context) : base(context) { }

		public bool CountryNameExists(string countryName)
			=> dbSet.Any(country => country.Name == countryName);
	}
}
