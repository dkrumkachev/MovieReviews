using BusinessLayer.Constants;
using BusinessLayer.Models.User;
using BusinessLayer.Services.Interfaces;
using ControllerLayer.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ControllerLayer.Controllers
{
	/// <summary>
	/// API controller for managing user accounts.
	/// </summary>
	[Route("api/users")]
	[ApiController]
	public class UserController : Controller
	{
		private readonly IUserService userService;
		private readonly IAuthenticationService authenticationService;

		public UserController(IUserService userService, IAuthenticationService authenticationService)
		{
			this.userService = userService;
			this.authenticationService = authenticationService;
		}

		/// <summary>
		/// Register a new user.
		/// </summary>
		/// <remarks>
		/// Registers a new user with the provided data.
		/// </remarks>
		/// <param name="user">The data for user registration.</param>
		/// <returns>Returns an AuthenticatedUserDTO representing the registered user.</returns>
		/// <response code="200">The user is successfully registered and authenticated
		/// and the authenticated user data is returned.</response>
		/// <response code="400">If the request is malformed, validation fails
		/// or a user with the same username already exists.</response>
		[HttpPost("register")]
		[AllowAnonymous]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		[ProducesResponseType(typeof(AuthenticatedUserDTO), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Register([FromBody] UserRegisterDTO user)
		{
			var authenticatedUser = authenticationService.Register(user);
			return Ok(authenticatedUser);
		}

		/// <summary>
		/// Log in a user.
		/// </summary>
		/// <remarks>
		/// Logs in a user with the provided credentials.
		/// </remarks>
		/// <param name="user">The credentials for user login.</param>
		/// <returns>Returns an AuthenticatedUserDTO representing the authenticated user.</returns>
		/// <response code="200">The user is successfully authenticated and the authenticated user data is returned.</response>
		/// <response code="404">If the user with the given username is not found.</response>
		/// <response code="401">If the password is incorrect.</response>
		/// <response code="400">If the request is malformed or validation fails.</response>
		[HttpPost("login")]
		[AllowAnonymous]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		[ProducesResponseType(typeof(AuthenticatedUserDTO), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public IActionResult LogIn([FromBody] UserLogInDTO user)
		{
			var authenticatedUser = authenticationService.LogIn(user);
			return Ok(authenticatedUser);
		}

		/// <summary>
		/// Get a user by their unique identifier.
		/// </summary>
		/// <param name="userId">The unique identifier of the user.</param>
		/// <returns>Returns a UserDTO representing the user.</returns>
		/// <response code="200">Returns the requested user.</response>
		/// <response code="404">If the user with the given ID is not found.</response>
		[HttpGet("{userId}")]
		[AllowAnonymous]
		[ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetUserById(int userId)
		{
			var user = userService.GetById(userId);
			return Ok(user);
		}

		/// <summary>
		/// Update the password for the authenticated user.
		/// </summary>
		/// <remarks>
		/// This action allows an authenticated user to update their password.
		/// </remarks>
		/// <param name="user">The new password data.</param>
		/// <response code="204">The password is successfully updated with no content returned.</response>
		/// <response code="401">If the user is not authenticated.</response>
		/// <response code="400">If the request is malformed or validation fails.</response>
		[HttpPut("new-password")]
		[Authorize]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult UpdatePassword([FromBody] UserUpdatePasswordDTO user)
		{
			var userId = int.Parse(User.FindFirstValue(CustomClaimTypes.Id)!);
			userService.Update(userId, user);
			return NoContent();
		}

		/// <summary>
		/// Update the username for the authenticated user.
		/// </summary>
		/// <remarks>
		/// This action allows an authenticated regular user to update their username.
		/// </remarks>
		/// <param name="user">The new username data.</param>
		/// <returns>Returns a UserDTO representing the updated user with the new username.</returns>
		/// <response code="200">The username is successfully updated and the updated user with the new username is returned.</response>
		/// <response code="401">If the user is not authenticated.</response>
		/// <response code="403">If the user is Admin.</response>
		/// <response code="400">If the request is malformed, validation fails or 
		/// a user with the same username already exists.</response>
		[HttpPut("new-username")]
		[Authorize(Roles = RoleNames.User)]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		[ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult UpdateUsername([FromBody] UserUpdateUsernameDTO user)
		{
			var userId = int.Parse(User.FindFirstValue(CustomClaimTypes.Id)!);
			var updatedUser = userService.Update(userId, user);
			return Ok(updatedUser);
		}

		/// <summary>
		/// Delete the authenticated user.
		/// </summary>
		/// <remarks>
		/// This action allows an authenticated regular user to delete their own account.
		/// </remarks>
		/// <response code="204">The user is successfully deleted with no content returned.</response>
		/// <response code="401">If the user is not authenticated.</response>
		/// <response code="403">If the user is Admin.</response>
		[HttpDelete]
		[Authorize(Roles = RoleNames.User)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public IActionResult DeleteUser()
		{
			var userId = int.Parse(User.FindFirstValue(CustomClaimTypes.Id)!);
			userService.Delete(userId);
			return NoContent();
		}
	}
}
