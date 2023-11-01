using BusinessLayer.Models.User;
using FluentValidation;

namespace BusinessLayer.Infrastructure.Validators.User
{
	public class UserRegisterDTOValidator : AbstractValidator<UserRegisterDTO>
	{
        public UserRegisterDTOValidator()
        {
			RuleFor(dto => dto.Username).NotEmpty().MaximumLength(100);
			RuleFor(dto => dto.Password).NotEmpty().Length(8, 100);
		}
    }
}
