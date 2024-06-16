using System.Net;
using AuctionService.Helpers.Exceptions;

namespace AuctionService.Middleware;

public class ExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var error = new Error
            {
                StatusCode = context.Response.StatusCode.ToString(),
                Message = e.Message
            };

            await context.Response.WriteAsJsonAsync(error.ToString());
        }
    }

}


public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}