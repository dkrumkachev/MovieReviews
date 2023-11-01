using BusinessLayer.Models.Movie;
using FluentValidation;

namespace BusinessLayer.Infrastructure.Validators.Movie
{
	public class MovieCreateDTOValidator : AbstractValidator<MovieCreateDTO>
	{
		public MovieCreateDTOValidator()
		{
			RuleFor(dto => dto.Year).GreaterThanOrEqualTo(1888).LessThan(2100);
			RuleFor(dto => dto.DirectorId).NotEmpty();
			RuleFor(dto => dto.CountryId).NotEmpty();
			RuleFor(dto => dto.Title).NotEmpty();
		}
	}
}