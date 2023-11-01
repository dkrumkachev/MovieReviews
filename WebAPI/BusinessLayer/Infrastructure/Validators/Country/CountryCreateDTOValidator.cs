using FluentValidation;
using BusinessLayer.Models.Country;

namespace BusinessLayer.Infrastructure.Validators.Country
{
	public class CountryCreateDTOValidator : AbstractValidator<CountryCreateDTO>
	{
        public CountryCreateDTOValidator()
        {
            RuleFor(dto => dto.Name).NotEmpty().MaximumLength(100);
        }
    }
}
