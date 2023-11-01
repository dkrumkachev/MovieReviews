using BusinessLayer.Models.Country;
using FluentValidation;

namespace BusinessLayer.Infrastructure.Validators.Country
{
	public class CountryUpdateDTOValidator : AbstractValidator<CountryUpdateDTO>
	{
		public CountryUpdateDTOValidator()
		{
			RuleFor(dto => dto.Name).NotEmpty().MaximumLength(100);
		}
	}
}