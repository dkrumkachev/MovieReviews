using BusinessLayer.Models.Review;
using FluentValidation;

namespace BusinessLayer.Infrastructure.Validators.Review
{
	public class ReviewCreateDTOValidator : AbstractValidator<ReviewCreateDTO>
	{
		public ReviewCreateDTOValidator()
		{
			RuleFor(dto => dto.Text).MaximumLength(10000);
			RuleFor(dto => dto.UserId).NotEmpty();
			RuleFor(dto => dto.MovieId).NotEmpty();
			RuleFor(dto => dto.Rating).GreaterThanOrEqualTo(1).LessThanOrEqualTo(10);
		}
	}
}