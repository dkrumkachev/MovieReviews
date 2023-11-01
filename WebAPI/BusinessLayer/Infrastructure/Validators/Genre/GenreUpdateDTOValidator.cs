using BusinessLayer.Models.Genre;
using FluentValidation;

namespace BusinessLayer.Infrastructure.Validators.Genre
{
	public class GenreUpdateDTOValidator : AbstractValidator<GenreUpdateDTO>
	{
		public GenreUpdateDTOValidator()
		{
			RuleFor(dto => dto.Name).NotEmpty().MaximumLength(50);
		}
	}
}