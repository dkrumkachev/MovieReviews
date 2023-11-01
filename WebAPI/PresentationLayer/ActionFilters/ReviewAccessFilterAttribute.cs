using BusinessLayer.Constants;
using BusinessLayer.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace PresentationLayer.ActionFilters
{
	public class ReviewAccessFilterAttribute : IActionFilter
	{

		private readonly IReviewService reviewService;

		public ReviewAccessFilterAttribute(IReviewService reviewService)
		{
			this.reviewService = reviewService;
		}

		public void OnActionExecuted(ActionExecutedContext context) { }

		public void OnActionExecuting(ActionExecutingContext context)
		{
			var userId = int.Parse(context.HttpContext.User.FindFirstValue(CustomClaimTypes.Id)!);
			var reviewId = (int)context.ActionArguments["reviewId"]!;
			var review = reviewService.GetById(reviewId);
			if (userId != review.UserId)
			{
				context.Result = new ObjectResult("Forbidden")
				{
					StatusCode = StatusCodes.Status403Forbidden
				};
			}
		}
	}
}
