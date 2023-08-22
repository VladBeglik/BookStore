using System.Net;
using BookStore.App.Infrastructure.Exceptions;

namespace BookStore.API.Middlewares;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            if (ex is CustomValidationException validationException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    errors = validationException.Failures
                };

                context.Response.WriteAsJsonAsync(errorResponse).Wait();
            }
            else if (ex is ICustomExceptionMarker)
            {
                context.Response.Headers.Add("X-Be-Error", "1");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                if (ex is CustomException customException)
                {
                    var errorResponse = new
                    {
                        error = customException.Message
                    };

                    context.Response.WriteAsJsonAsync(errorResponse).Wait();
                }
            }
            else
            {
                var code = HttpStatusCode.InternalServerError;

                _logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)code;

                var errorResponse = new
                {
                    error = new[] { ex.Message },
                    stackTrace = ex.StackTrace
                };

                context.Response.WriteAsJsonAsync(errorResponse).Wait();
            }
        }
    }
}
