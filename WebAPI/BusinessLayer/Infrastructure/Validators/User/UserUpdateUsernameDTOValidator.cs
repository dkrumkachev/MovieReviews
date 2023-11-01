using BusinessLayer.Models.User;
using FluentValidation;

namespace BusinessLayer.Infrastructure.Validators.User
{
	public class UserUpdateUsernameDTOValidator : AbstractValidator<UserUpdateUsernameDTO>
	{
		public UserUpdateUsernameDTOValidator()
		{
			RuleFor(dto => dto.Username).NotEmpty().MaximumLength(100);
		}
	}
}