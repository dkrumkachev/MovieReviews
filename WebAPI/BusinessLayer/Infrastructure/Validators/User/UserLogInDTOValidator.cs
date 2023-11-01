using BusinessLayer.Models.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Infrastructure.Validators.User
{
	public class UserLogInDTOValidator : AbstractValidator<UserLogInDTO>
	{
		public UserLogInDTOValidator() 
		{
			RuleFor(dto => dto.Username).NotEmpty();
			RuleFor(dto => dto.Password).NotEmpty();
		}
	}
}
