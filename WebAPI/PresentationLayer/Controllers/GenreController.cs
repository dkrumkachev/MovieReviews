using BusinessLayer.Constants;
using BusinessLayer.Models.Genre;
using BusinessLayer.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.ActionFilters;

namespace PresentationLayer.Controllers
{
	/// <summary>
	/// API controller for managing genres-related operations.
	/// </summary>
	[Route("api/genres")]
	[ApiController]
	public class GenreController : Controller
	{
		private readonly IGenreService genreService;

		public GenreController(IGenreService genreService)
		{
			this.genreService = genreService;
		}

		/// <summary>
		/// Get a list of genres.
		/// </summary>
		/// <returns>Returns an IEnumerable of GenreDTO representing the list of genres.</returns>
		/// <response code="200">Returns the list of genres.</response>
		[HttpGet]
		[AllowAnonymous]
		[ProducesResponseType(typeof(IEnumerable<GenreDTO>), StatusCodes.Status200OK)]
		public IActionResult GetGenres()
		{
			var genres = genreService.GetAll();
			return Ok(genres);
		}

		/// <summary>
		/// Get a genre by their unique identifier.
		/// </summary>
		/// <param name="genreId">The unique identifier of the genre.</param>
		/// <returns>Returns a GenreDTO representing the genre.</returns>
		/// <response code="200">Returns the requested genre.</response>
		/// <response code="404">If the genre with the given ID is not found.</response>
		[HttpGet("{genreId}")]
		[AllowAnonymous]
		[ProducesResponseType(typeof(GenreDTO), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetGenreById(int genreId)
		{
			var genre = genreService.GetById(genreId);
			return Ok(genre);
		}

		/// <summary>
		/// Create a new genre with administrative access.
		/// </summary>
		/// <remarks>
		/// Creates a new genre with the provided data. Only users with the "Admin" role can perform this action.
		/// </remarks>
		/// <param name="genre">The data to create the new genre.</param>
		/// <returns>Returns the created GenreDTO representing the newly created genre.</returns>
		/// <response code="201">The genre is successfully created and the newly created genre is returned.</response>
		/// <response code="400">If the request is malformed, validation fails
		/// or a genre with the same name already exists.</response>
		/// <response code="401">If the user is not authenticated.</response>
		/// <response code="403">If the user does not have the necessary permissions to create a genre.</response>
		[HttpPost]
		[Authorize(Roles = RoleNames.Admin)]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		[ProducesResponseType(typeof(GenreDTO), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public IActionResult CreateGenre([FromBody] GenreCreateDTO genre)
		{
			var createdGenre = genreService.Create(genre);
			return CreatedAtAction(nameof(GetGenreById), new { genreId = createdGenre.Id }, createdGenre);
		}

		/// <summary>
		/// Update an existing genre with administrative access.
		/// </summary>
		/// <remarks>
		/// Updates an existing genre with the provided data. Only users with the "Admin" role can perform this action.
		/// </remarks>
		/// <param name="genreId">The unique identifier of the genre to update.</param>
		/// <param name="genre">The data to update the genre with.</param>
		/// <returns>Returns the updated GenreDTO representing the modified genre.</returns>
		/// <response code="200">The genre is successfully updated and the updated genre is returned.</response>
		/// <response code="404">If the genre with the given ID is not found.</response>
		/// <response code="400">If the request is malformed, validation fails
		/// or a genre with the same name already exists.</response>
		/// <response code="401">If the user is not authenticated.</response>
		/// <response code="403">If the user does not have the necessary permissions to update a genre.</response>
		[HttpPut("{genreId}")]
		[Authorize(Roles = RoleNames.Admin)]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		[ProducesResponseType(typeof(GenreDTO), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public IActionResult UpdateGenre(int genreId, [FromBody] GenreUpdateDTO genre)
		{
			var updatedGenre = genreService.Update(genreId, genre);
			return Ok(updatedGenre);
		}

		/// <summary>
		/// Delete a genre with administrative access.
		/// </summary>
		/// <remarks>
		/// Deletes a genre with the specified ID. Only users with the "Admin" role can perform this action.
		/// </remarks>
		/// <param name="genreId">The unique identifier of the genre to delete.</param>
		/// <response code="204">The genre is successfully deleted with no content returned.</response>
		/// <response code="404">If the genre with the given ID is not found.</response>
		/// <response code="401">If the user is not authenticated.</response>
		/// <response code="403">If the user does not have the necessary permissions to delete a genre.</response>
		[HttpDelete("{genreId}")]
		[Authorize(Roles = RoleNames.Admin)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public IActionResult DeleteGenre(int genreId)
		{
			genreService.Delete(genreId);
			return NoContent();
		}
	}
}
