using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PresentationLayer.ActionFilters
{
	public class ValidationFilterAttribute : IActionFilter
	{
		public void OnActionExecuted(ActionExecutedContext context) { }

		public void OnActionExecuting(ActionExecutingContext context)
		{
			var action = context.RouteData.Values["action"];
			var controller = context.RouteData.Values["controller"];
			var dto = context.ActionArguments.SingleOrDefault(x =>
				(x.Value?.ToString() ?? string.Empty).Contains("DTO")).Value;
			if (dto == null)
			{
				context.Result = new BadRequestObjectResult($"Object is null. Controller: {controller}, action: {action}.");
				return;
			}
			if (!context.ModelState.IsValid)
			{
				context.Result = new UnprocessableEntityObjectResult(context.ModelState);
			}
		}
	}
}
