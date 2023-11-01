using BusinessLayer.Models.Director;
using FluentValidation;

namespace BusinessLayer.Infrastructure.Validators.Director
{
	public class DirectorUpdateDTOValidator : AbstractValidator<DirectorUpdateDTO>
	{
		public DirectorUpdateDTOValidator()
		{
			RuleFor(dto => dto.Name).NotEmpty().MaximumLength(100);
		}
	}
}