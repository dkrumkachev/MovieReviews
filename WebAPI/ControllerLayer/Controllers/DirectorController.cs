using BusinessLayer.Constants;
using BusinessLayer.Models.Director;
using BusinessLayer.Services.Interfaces;
using ControllerLayer.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControllerLayer.Controllers
{
	/// <summary>
	/// API controller for managing directors-related operations.
	/// </summary>
	[Route("api/directors")]
	[ApiController]
	public class DirectorController : Controller
	{
		private readonly IDirectorService directorService;

		public DirectorController(IDirectorService directorService)
		{
			this.directorService = directorService;
		}

		/// <summary>
		/// Get a list of directors with administrative access.
		/// </summary>
		/// <remarks>
		/// Retrieves a list of directors. Only users with the "Admin" role can perform this action.
		/// </remarks>
		/// <returns>Returns an IEnumerable of DirectorDTO representing the list of directors.</returns>
		/// <response code="200">Returns the list of directors.</response>
		/// <response code="401">If the user is not authenticated.</response>
		/// <response code="403">If the user does not have the necessary permissions to access the directors.</response>
		[HttpGet]
		[Authorize(Roles = RoleNames.Admin)]
		[ProducesResponseType(typeof(IEnumerable<DirectorDTO>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public IActionResult GetDirectors()
		{
			var directors = directorService.GetAll();
			return Ok(directors);
		}

		/// <summary>
		/// Get a director by their unique identifier.
		/// </summary>
		/// <param name="directorId">The unique identifier of the director.</param>
		/// <returns>Returns a DirectorDTO representing the director.</returns>
		/// <response code="200">Returns the requested director.</response>
		/// <response code="404">If the director with the given ID is not found.</response>
		[HttpGet("{directorId}")]
		[AllowAnonymous]
		[ProducesResponseType(typeof(DirectorDTO), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetDirectorById(int directorId)
		{
			var director = directorService.GetById(directorId);
			return Ok(director);
		}

		/// <summary>
		/// Create a new director with administrative access.
		/// </summary>
		/// <remarks>
		/// Creates a new director with the provided data. Only users with the "Admin" role can perform this action.
		/// </remarks>
		/// <param name="director">The data to create the new director.</param>
		/// <returns>Returns the created DirectorDTO representing the newly created director.</returns>
		/// <response code="201">The director is successfully created and the newly created director is returned.</response>
		/// <response code="400">If the request is malformed or validation fails.</response>
		/// <response code="401">If the user is not authenticated.</response>
		/// <response code="403">If the user does not have the necessary permissions to create a director.</response>
		[HttpPost]
		[Authorize(Roles = RoleNames.Admin)]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		[ProducesResponseType(typeof(DirectorDTO), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public IActionResult CreateDirector([FromBody] DirectorCreateDTO director)
		{
			var createdDirector = directorService.Create(director);
			return CreatedAtAction(nameof(GetDirectorById), new { directorId = createdDirector.Id }, createdDirector);
		}

		/// <summary>
		/// Update an existing director with administrative access.
		/// </summary>
		/// <remarks>
		/// Updates an existing director with the provided data. Only users with the "Admin" role can perform this action.
		/// </remarks>
		/// <param name="directorId">The unique identifier of the director to update.</param>
		/// <param name="director">The data to update the director with.</param>
		/// <returns>Returns the updated DirectorDTO representing the modified director.</returns>
		/// <response code="200">The director is successfully updated and the updated director is returned.</response>
		/// <response code="404">If the director with the given ID is not found.</response>
		/// <response code="400">If the request is malformed or validation fails.</response>
		/// <response code="401">If the user is not authenticated.</response>
		/// <response code="403">If the user does not have the necessary permissions to update a director.</response>
		[HttpPut("{directorId}")]
		[Authorize(Roles = RoleNames.Admin)]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		[ProducesResponseType(typeof(DirectorDTO), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public IActionResult UpdateDirector(int directorId, [FromBody] DirectorUpdateDTO director)
		{
			var updatedDirector = directorService.Update(directorId, director);
			return Ok(updatedDirector);
		}

		/// <summary>
		/// Delete a director with administrative access.
		/// </summary>
		/// <remarks>
		/// Deletes a director with the specified ID. Only users with the "Admin" role can perform this action.
		/// </remarks>
		/// <param name="directorId">The unique identifier of the director to delete.</param>
		/// <response code="204">The director is successfully deleted with no content returned.</response>
		/// <response code="404">If the director with the given ID is not found.</response>
		/// <response code="401">If the user is not authenticated.</response>
		/// <response code="403">If the user does not have the necessary permissions to delete a director.</response>
		[HttpDelete("{directorId}")]
		[Authorize(Roles = RoleNames.Admin)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public IActionResult DeleteDirector(int directorId)
		{
			directorService.Delete(directorId);
			return NoContent();
		}

	}
}
