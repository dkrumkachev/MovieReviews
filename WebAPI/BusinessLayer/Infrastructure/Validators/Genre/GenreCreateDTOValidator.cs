using BusinessLayer.Models.Genre;
using FluentValidation;

namespace BusinessLayer.Infrastructure.Validators.Genre
{
	public class GenreCreateDTOValidator : AbstractValidator<GenreCreateDTO>
	{
		public GenreCreateDTOValidator()
		{
			RuleFor(dto => dto.Name).NotEmpty().MaximumLength(50);
		}
	}
}