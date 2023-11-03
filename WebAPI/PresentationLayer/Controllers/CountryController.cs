using BusinessLayer.Models.Country;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Constants;
using ControllerLayer.ActionFilters;
using BusinessLayer.Services.Interfaces;

namespace ControllerLayer.Controllers
{
	/// <summary>
	/// API controller for managing countries-related operations.
	/// </summary>
	[Route("api/countries")]
	[ApiController]
	public class CountryController : Controller
	{
		private readonly ICountryService countryService;

		public CountryController(ICountryService countryService)
		{
			this.countryService = countryService;
		}

		/// <summary>
		/// Get a list of countries with administrative access.
		/// </summary>
		/// <remarks>
		/// Retrieves a list of countries. Only users with the "Admin" role can perform this action
		/// </remarks>
		/// <returns>Returns an IEnumerable of CountryDTO representing the list of countries.</returns>
		/// <response code="200">Returns the list of countries.</response>
		/// <response code="401">If the user is not authenticated.</response>
		/// <response code="403">If the user does not have the necessary permissions to get all countries.</response>
		[HttpGet]
		[Authorize(Roles = RoleNames.Admin)]
		[ProducesResponseType(typeof(IEnumerable<CountryDTO>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public IActionResult GetCountries()
		{
			var countries = countryService.GetAll();
			return Ok(countries);
		}

		/// <summary>
		/// Get a country by its unique identifier.
		/// </summary>
		/// <remarks>
		/// Retrieves a country based on its ID.
		/// </remarks>
		/// <param name="countryId">The unique identifier of the country.</param>
		/// <returns>Returns a CountryDTO object representing the country.</returns>
		/// <response code="200">Returns the requested country.</response>
		/// <response code="404">If the country with the given ID is not found.</response>
		[HttpGet("{countryId}")]
		[AllowAnonymous]
		[ProducesResponseType(typeof(CountryDTO), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetCountryById(int countryId)
		{
			var country = countryService.GetById(countryId);
			return Ok(country);
		}

		/// <summary>
		/// Create a new country with administrative access.
		/// </summary>
		/// <remarks>
		/// Creates a new country with the provided data. Only users with the "Admin" role can perform this action.
		/// </remarks>
		/// <param name="country">The data to create the new country.</param>
		/// <returns>Returns the created CountryDTO representing the newly created country.</returns>
		/// <response code="201">The country is successfully created and the newly created country is returned.</response>
		/// <response code="400">If the request is malformed, 
		///	validation fails or a country with the same name already exists.</response>
		/// <response code="401">If the user is not authenticated.</response>
		/// <response code="403">If the user does not have the necessary permissions to create a country.</response>
		[HttpPost]
		[Authorize(Roles = RoleNames.Admin)]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		[ProducesResponseType(typeof(CountryDTO), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public IActionResult CreateCountry([FromBody] CountryCreateDTO country)
		{
			var createdCountry = countryService.Create(country);
			return CreatedAtAction(nameof(GetCountryById), new { countryId = createdCountry.Id }, createdCountry);
		}

		/// <summary>
		/// Update an existing country with administrative access.
		/// </summary>
		/// <remarks>
		/// Updates an existing country with the provided data. Only users with the "Admin" role can perform this action.
		/// </remarks>
		/// <param name="countryId">The unique identifier of the country to update.</param>
		/// <param name="country">The data to update the country with.</param>
		/// <returns>Returns the updated CountryDTO representing the modified country.</returns>
		/// <response code="200">The country is successfully updated and the updated country is returned.</response>
		/// <response code="404">If the country with the given ID is not found.</response>
		/// <response code="400">If the request is malformed, validation fails
		/// or a country with the same name already exists.</response>
		/// <response code="401">If the user is not authenticated.</response>
		/// <response code="403">If the user does not have the necessary permissions to update a country.</response>
		[HttpPut("{countryId}")]
		[Authorize(Roles = RoleNames.Admin)]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		[ProducesResponseType(typeof(CountryDTO), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public IActionResult UpdateCountry(int countryId, [FromBody] CountryUpdateDTO country)
		{
			var updatedCountry = countryService.Update(countryId, country);
			return Ok(updatedCountry);
		}

		/// <summary>
		/// Delete a country with administrative access.
		/// </summary>
		/// <remarks>
		/// Deletes a country with the specified ID. Only users with the "Admin" role can perform this action.
		/// </remarks>
		/// <param name="countryId">The unique identifier of the country to delete.</param>
		/// <response code="204">The country is successfully deleted with no content returned.</response>
		/// <response code="404">If the country with the given ID is not found.</response>
		/// <response code="401">If the user is not authenticated.</response>
		/// <response code="403">If the user does not have the necessary permissions to delete a country.</response>
		[HttpDelete("{countryId}")]
		[Authorize(Roles = RoleNames.Admin)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public IActionResult DeleteCountry(int countryId)
		{
			countryService.Delete(countryId);
			return NoContent();
		}
	}
}
