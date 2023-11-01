using AutoMapper;
using BusinessLayer.Exceptions;
using BusinessLayer.Models.User;
using BusinessLayer.Services.Contracts;
using DataLayer.Models;
using DataLayer.Repositories.UnitOfWork;

namespace BusinessLayer.Services.Implementations
{
	public class UserService : IUserService
	{
		private readonly IRepositoryManager repositoryManager;
		private readonly IMapper mapper;
		private readonly IPasswordService passwordService;

		public UserService(IRepositoryManager repositoryManager, IMapper mapper, IPasswordService passwordService)
		{
			this.repositoryManager = repositoryManager;
			this.mapper = mapper;
			this.passwordService = passwordService;
		}

		public User Create(UserRegisterDTO user)
		{
			if (repositoryManager.Users.UsernameExists(user.Username))
			{
				throw new EntityAlreadyExistsException($"Username '{user.Username}' is already taken.");
			}
			user.Password = passwordService.HashPassword(user.Password);
			var newUser = mapper.Map<User>(user);
			repositoryManager.Users.Create(newUser);
			repositoryManager.Save();
			return newUser;
		}

		public UserDTO Update(int userId, UserUpdateUsernameDTO user)
		{
			var userToUpdate = repositoryManager.Users.GetById(userId)
				?? throw new EntityNotFoundException($"User with id {userId} does not exist.");
			if (userToUpdate.Username != user.Username && repositoryManager.Users.UsernameExists(user.Username))
			{
				throw new EntityAlreadyExistsException($"Username '{user.Username}' is already taken.");
			}
			mapper.Map(user, userToUpdate);
			repositoryManager.Users.Update(userToUpdate);
			repositoryManager.Save();
			return mapper.Map<UserDTO>(userToUpdate);
		}

		public UserDTO Update(int userId, UserUpdatePasswordDTO user)
		{
			var userToUpdate = repositoryManager.Users.GetById(userId)
				?? throw new EntityNotFoundException($"User with id {userId} does not exist.");
			user.Password = passwordService.HashPassword(user.Password);
			mapper.Map(user, userToUpdate);
			repositoryManager.Users.Update(userToUpdate);
			repositoryManager.Save();
			return mapper.Map<UserDTO>(userToUpdate);
		}

		public void Delete(int userId)
		{
			var userToDelete = repositoryManager.Users.GetById(userId)
				?? throw new EntityNotFoundException($"User with id {userId} does not exist.");
			repositoryManager.Users.Delete(userToDelete);
			repositoryManager.Save();
		}

		public UserDTO GetById(int userId)
		{
			var user = repositoryManager.Users.GetById(userId)
				?? throw new EntityNotFoundException($"User with id {userId} does not exist.");
			return mapper.Map<UserDTO>(user);
		}
	}
}
