using BusinessLayer.Constants;
using BusinessLayer.Models.Review;
using BusinessLayer.Services.Interfaces;
using ControllerLayer.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ControllerLayer.Controllers
{
	/// <summary>
	/// API controller for managing reviews-related operations.
	/// </summary>
	[Route("api/reviews")]
	[ApiController]
	public class ReviewController : Controller
	{
		private readonly IReviewService reviewService;
		private readonly IUserService userService;

		public ReviewController(IReviewService reviewService, IUserService userService)
		{
			this.reviewService = reviewService;
			this.userService = userService;
		}

		/// <summary>
		/// Get a review by its unique identifier.
		/// </summary>
		/// <param name="reviewId">The unique identifier of the review.</param>
		/// <returns>Returns a ReviewDTO representing the review.</returns>
		/// <response code="200">Returns the requested review.</response>
		/// <response code="404">If the review with the given ID is not found.</response>
		[HttpGet("{reviewId}")]
		[AllowAnonymous]
		[ProducesResponseType(typeof(ReviewDTO), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetReviewById(int reviewId)
		{
			var review = reviewService.GetById(reviewId);
			return Ok(review);
		}

		/// <summary>
		/// Get a list of reviews by user.
		/// </summary>
		/// <param name="userId">The unique identifier of the user for which to retrieve reviews.</param>
		/// <returns>Returns an IEnumerable of ReviewDTO representing the list of reviews by the specified user.</returns>
		/// <response code="200">Returns the list of reviews by the specified user.</response>
		/// <response code="404">If the user with the given ID is not found.</response>
		[HttpGet("user/{userId}")]
		[AllowAnonymous]
		[ProducesResponseType(typeof(IEnumerable<ReviewDTO>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetReviewsByUser(int userId)
		{
			var reviews = reviewService.GetByUser(userId);
			return Ok(reviews);
		}

		/// <summary>
		/// Create a new review with user access.
		/// </summary>
		/// <remarks>
		/// Creates a new review with the provided data. Only authenticated users can perform this action.
		/// </remarks>
		/// <param name="review">The data to create the new review.</param>
		/// <returns>Returns the created ReviewDTO representing the newly created review.</returns>
		/// <response code="201">The review is successfully created and the newly created review is returned.</response>
		/// <response code="404">If the user or movie is not found.</response>
		/// <response code="400">If the request is malformed, validation fails 
		/// or the user already has a review for the specified movie.</response>
		/// <response code="401">If the user is not authenticated.</response>
		/// <response code="403">If the user is Admin or the user's id does not match the authenticated user.</response>
		[HttpPost]
		[Authorize(Roles = RoleNames.User)]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		[ProducesResponseType(typeof(ReviewDTO), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public IActionResult CreateReview([FromBody] ReviewCreateDTO review)
		{
			var authorizedUser = int.Parse(User.FindFirstValue(CustomClaimTypes.Id)!);
			if (review.UserId != authorizedUser)
			{
				return StatusCode(StatusCodes.Status403Forbidden, "Forbidden");
			}
			var createdReview = reviewService.Create(review);
			return CreatedAtAction(nameof(GetReviewById), new { reviewId = createdReview.Id }, createdReview);
		}

		/// <summary>
		/// Update an existing review with user access.
		/// </summary>
		/// <remarks>
		/// Updates an existing review with the provided data. 
		/// Only authenticated users can perform this action and only for their own reviews.
		/// </remarks>
		/// <param name="reviewId">The unique identifier of the review to update.</param>
		/// <param name="review">The data to update the review with.</param>
		/// <returns>Returns the updated ReviewDTO representing the modified review.</returns>
		/// <response code="200">The review is successfully updated and the updated review is returned.</response>
		/// <response code="404">If the review with the given ID is not found.</response>
		/// <response code="400">If the request is malformed or validation fails.</response>
		/// <response code="401">If the user is not authenticated.</response>
		/// <response code="403">If the user is Admin or the review does not belong to the user.</response>
		[HttpPut("{reviewId}")]
		[Authorize(Roles = RoleNames.User)]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		[ServiceFilter(typeof(ReviewAccessFilterAttribute))]
		[ProducesResponseType(typeof(ReviewDTO), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public IActionResult UpdateReview(int reviewId, [FromBody] ReviewUpdateDTO review)
		{
			var updatedReview = reviewService.Update(reviewId, review);
			return Ok(updatedReview);
		}

		/// <summary>
		/// Delete a review with user access.
		/// </summary>
		/// <remarks>
		/// Deletes a review with the specified ID.
		/// Only authenticated users can perform this action and only for their own reviews.
		/// </remarks>
		/// <param name="reviewId">The unique identifier of the review to delete.</param>
		/// <response code="204">The review is successfully deleted with no content returned.</response>
		/// <response code="404">If the review with the given ID is not found.</response>
		/// <response code="401">If the user is not authenticated.</response>
		/// <response code="403">If the user is Admin or the review does not belong to the user.</response>
		[HttpDelete("{reviewId}")]
		[Authorize(Roles = RoleNames.User)]
		[ServiceFilter(typeof(ReviewAccessFilterAttribute))]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public IActionResult DeleteReview(int reviewId)
		{
			reviewService.Delete(reviewId);
			return NoContent();
		}

	}
}
