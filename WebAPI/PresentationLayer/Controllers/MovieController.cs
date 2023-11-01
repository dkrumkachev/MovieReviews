using BusinessLayer.Constants;
using BusinessLayer.Models.Movie;
using BusinessLayer.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.ActionFilters;

namespace PresentationLayer.Controllers
{
	/// <summary>
	/// API controller for managing movies-related operations.
	/// </summary>
	[Route("api/movies")]
	[ApiController]
	public class MovieController : Controller
	{
		private readonly IMovieService movieService;

		public MovieController(IMovieService movieService)
		{
			this.movieService = movieService;
		}

		/// <summary>
		/// Get a list of movies.
		/// </summary>
		/// <returns>Returns an IEnumerable of MovieDTO representing the list of movies.</returns>
		/// <response code="200">Returns the list of movies.</response>
		[HttpGet]
		[AllowAnonymous]
		[ProducesResponseType(typeof(IEnumerable<MovieDTO>), StatusCodes.Status200OK)]
		public IActionResult GetMovies()
		{
			var movies = movieService.GetAll();
			return Ok(movies);
		}

		/// <summary>
		/// Get a movie by its unique identifier.
		/// </summary>
		/// <param name="movieId">The unique identifier of the movie.</param>
		/// <returns>Returns a MovieDTO representing the movie.</returns>
		/// <response code="200">Returns the requested movie.</response>
		/// <response code="404">If the movie with the given ID is not found.</response>
		[HttpGet("{movieId}")]
		[AllowAnonymous]
		[ProducesResponseType(typeof(MovieDTO), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetMovieById(int movieId)
		{
			var movie = movieService.GetById(movieId);
			return Ok(movie);
		}

		/// <summary>
		/// Get a list of movies by country.
		/// </summary>
		/// <param name="countryId">The unique identifier of the country for which to retrieve movies.</param>
		/// <returns>Returns an IEnumerable of MovieDTO representing the list of movies from the specified country.</returns>
		/// <response code="200">Returns the list of movies from the specified country.</response>
		/// <response code="404">If the country with the given ID is not found.</response>
		[HttpGet("country/{countryId}")]
		[AllowAnonymous]
		[ProducesResponseType(typeof(IEnumerable<MovieDTO>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetMoviesByCountry(int countryId)
		{
			var movies = movieService.GetByCountry(countryId);
			return Ok(movies);
		}

		/// <summary>
		/// Get a list of movies by director.
		/// </summary>
		/// <param name="directorId">The unique identifier of the director for which to retrieve movies.</param>
		/// <returns>Returns an IEnumerable of MovieDTO representing the list of movies directed by the specified director.</returns>
		/// <response code="200">Returns the list of movies directed by the specified director.</response>
		/// <response code="404">If the director with the given ID is not found.</response>
		[HttpGet("director/{directorId}")]
		[AllowAnonymous]
		[ProducesResponseType(typeof(IEnumerable<MovieDTO>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetMoviesByDirector(int directorId)
		{
			var movies = movieService.GetByDirector(directorId);
			return Ok(movies);
		}

		/// <summary>
		/// Get a list of movies by genre.
		/// </summary>
		/// <param name="genreId">The unique identifier of the genre for which to retrieve movies.</param>
		/// <returns>Returns an IEnumerable of MovieDTO representing the list of movies with the specified genre.</returns>
		/// <response code="200">Returns the list of movies with the specified genre.</response>
		/// <response code="404">If the genre with the given ID is not found.</response>
		[HttpGet("genre/{genreId}")]
		[AllowAnonymous]
		[ProducesResponseType(typeof(IEnumerable<MovieDTO>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetMoviesByGenre(int genreId)
		{
			var movies = movieService.GetByGenre(genreId);
			return Ok(movies);
		}

		/*
		[HttpGet]
		[AllowAnonymous]
		[ProducesResponseType(typeof(IEnumerable<MovieDTO>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetMoviesByRequestParameters(MovieParameters parameters)
		{
			var movies = movieService.GetByParameters(parameters);
			return Ok(movies);
		}
		*/

		/// <summary>
		/// Create a new movie with administrative access.
		/// </summary>
		/// <remarks>
		/// Creates a new movie with the provided data. Only users with the "Admin" role can perform this action.
		/// </remarks>
		/// <param name="movie">The data to create the new movie.</param>
		/// <returns>Returns the created MovieDTO representing the newly created movie.</returns>
		/// <response code="201">The movie is successfully created, and the newly created movie is returned.</response>
		/// <response code="400">If the request is malformed or validation fails.</response>
		/// <response code="401">If the user is not authenticated.</response>
		/// <response code="403">If the user does not have the necessary permissions to create a movie.</response>
		[HttpPost]
		[Authorize(Roles = RoleNames.Admin)]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		[ProducesResponseType(typeof(MovieDTO), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public IActionResult CreateMovie([FromBody] MovieCreateDTO movie)
		{
			var createdMovie = movieService.Create(movie);
			return CreatedAtAction(nameof(GetMovieById), new { movieId = createdMovie.Id }, createdMovie);
		}

		/// <summary>
		/// Update an existing movie with administrative access.
		/// </summary>
		/// <remarks>
		/// Updates an existing movie with the provided data. Only users with the "Admin" role can perform this action.
		/// </remarks>
		/// <param name="movieId">The unique identifier of the movie to update.</param>
		/// <param name="movie">The data to update the movie with.</param>
		/// <returns>Returns the updated MovieDTO representing the modified movie.</returns>
		/// <response code="200">The movie is successfully updated, and the updated movie is returned.</response>
		/// <response code="404">If the movie with the given ID is not found.</response>
		/// <response code="400">If the request is malformed or validation fails.</response>
		/// <response code="401">If the user is not authenticated.</response>
		/// <response code="403">If the user does not have the necessary permissions to update a movie.</response>
		[HttpPut("{movieId}")]
		[Authorize(Roles = RoleNames.Admin)]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		[ProducesResponseType(typeof(MovieDTO), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public IActionResult UpdateMovie(int movieId, [FromBody] MovieUpdateDTO movie)
		{
			var updatedMovie = movieService.Update(movieId, movie);
			return Ok(updatedMovie);
		}

		/// <summary>
		/// Delete a movie with administrative access.
		/// </summary>
		/// <remarks>
		/// Deletes a movie with the specified ID. Only users with the "Admin" role can perform this action.
		/// </remarks>
		/// <param name="movieId">The unique identifier of the movie to delete.</param>
		/// <response code="204">The movie is successfully deleted with no content returned.</response>
		/// <response code="404">If the movie with the given ID is not found.</response>
		/// <response code="401">If the user is not authenticated.</response>
		/// <response code="403">If the user does not have the necessary permissions to delete a movie.</response>
		[HttpDelete("{movieId}")]
		[Authorize(Roles = RoleNames.Admin)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public IActionResult DeleteMovie(int movieId)
		{
			movieService.Delete(movieId);
			return NoContent();
		}
	}
}
