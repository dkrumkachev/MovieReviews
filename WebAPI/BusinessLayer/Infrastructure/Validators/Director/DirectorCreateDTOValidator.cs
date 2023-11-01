using BusinessLayer.Models.Director;
using FluentValidation;

namespace BusinessLayer.Infrastructure.Validators.Director
{
	public class DirectorCreateDTOValidator : AbstractValidator<DirectorCreateDTO>
	{
		public DirectorCreateDTOValidator()
		{
			RuleFor(dto => dto.Name).NotEmpty().MaximumLength(100);
		}
	}
}