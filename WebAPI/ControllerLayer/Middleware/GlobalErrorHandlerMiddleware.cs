using BusinessLayer.Exceptions;
using FluentValidation;
using System.Net;
using ControllerLayer.ErrorModel;

namespace ControllerLayer.Middleware
{
	public class GlobalErrorHandlerMiddleware
	{
		private readonly RequestDelegate next;

		public GlobalErrorHandlerMiddleware(RequestDelegate next)
		{
			this.next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				var response = context.Response;
				response.ContentType = "application/json";
				response.StatusCode = ex switch
				{
					EntityAlreadyExistsException => (int)HttpStatusCode.BadRequest,
					EntityNotFoundException => (int)HttpStatusCode.NotFound,
					IncorrectPasswordException => (int)HttpStatusCode.Unauthorized,
					ValidationException => (int)HttpStatusCode.BadRequest,
					_ => (int)HttpStatusCode.InternalServerError,
				};
				await response.WriteAsync(new ErrorDetails
				{
					StatusCode = response.StatusCode,
					Message = ex.Message
				}
				.ToString());
			}
		}
	}

	public static class GlobalErrorHandlerMiddlewareExtension
	{
		public static IApplicationBuilder UseGlobalErrorHandler(this IApplicationBuilder app)
		{
			return app.UseMiddleware<GlobalErrorHandlerMiddleware>();
		}
	}
}
