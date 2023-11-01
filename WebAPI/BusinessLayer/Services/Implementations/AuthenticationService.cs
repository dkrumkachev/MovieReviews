using AutoMapper;
using BusinessLayer.Exceptions;
using BusinessLayer.Models.User;
using BusinessLayer.Services.Contracts;
using DataLayer.Repositories.UnitOfWork;

namespace BusinessLayer.Services.Implementations
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly IRepositoryManager repositoryManager;
		private readonly IUserService userService;
		private readonly IPasswordService passwordService;
		private readonly ITokenService tokenService;
		private readonly IMapper mapper;

		public AuthenticationService(IRepositoryManager repositoryManager, IUserService userService, 
			IPasswordService passwordService, ITokenService tokenService, IMapper mapper)
		{
			this.repositoryManager = repositoryManager;
			this.userService = userService;
			this.passwordService = passwordService;
			this.tokenService = tokenService;
			this.mapper = mapper;
		}

        public AuthenticatedUserDTO Register(UserRegisterDTO user)
		{
			var newUser = userService.Create(user);
			var authenticatedUser = mapper.Map<AuthenticatedUserDTO>(newUser);
			authenticatedUser.JwtToken = tokenService.CreateToken(authenticatedUser);
			return authenticatedUser;
		}

		public AuthenticatedUserDTO LogIn(UserLogInDTO user)
		{
			var storedUser = repositoryManager.Users.GetByUsername(user.Username)
				?? throw new EntityNotFoundException($"User with username '{user.Username}' does not exist.");
			if (!passwordService.VerifyPassword(storedUser.Password, user.Password))
			{
				throw new IncorrectPasswordException($"Incorrect password.");
			}
			var authenticatedUser = mapper.Map<AuthenticatedUserDTO>(storedUser);
			authenticatedUser.JwtToken = tokenService.CreateToken(authenticatedUser);
			return authenticatedUser;
		}
	}
}
