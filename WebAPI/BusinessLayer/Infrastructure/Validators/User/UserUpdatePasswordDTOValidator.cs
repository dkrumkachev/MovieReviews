using BusinessLayer.Models.User;
using FluentValidation;

namespace BusinessLayer.Infrastructure.Validators.User
{
	public class UserUpdatePasswordDTOValidator : AbstractValidator<UserUpdatePasswordDTO>
	{
        public UserUpdatePasswordDTOValidator()
        {
			RuleFor(dto => dto.Password).NotEmpty().MinimumLength(8);
		}
	}
}
