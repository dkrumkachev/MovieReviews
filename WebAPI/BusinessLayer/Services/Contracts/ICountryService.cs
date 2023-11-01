using BusinessLayer.Models.Country;

namespace BusinessLayer.Services.Contracts
{
	public interface ICountryService
	{
		CountryDTO Create(CountryCreateDTO country);

		CountryDTO Update(int countryId, CountryUpdateDTO country);

		void Delete(int countryId);

		CountryDTO GetById(int countryId);

		IEnumerable<CountryDTO> GetAll();
	}
}
