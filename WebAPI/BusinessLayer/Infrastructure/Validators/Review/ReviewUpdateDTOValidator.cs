using BusinessLayer.Models.Review;
using FluentValidation;

namespace BusinessLayer.Infrastructure.Validators.Review
{
	public class ReviewUpdateDTOValidator : AbstractValidator<ReviewUpdateDTO>
	{
		public ReviewUpdateDTOValidator()
		{
			RuleFor(dto => dto.Text).MaximumLength(10000);
			RuleFor(dto => dto.Rating).GreaterThanOrEqualTo(1).LessThanOrEqualTo(10);
		}
	}
}