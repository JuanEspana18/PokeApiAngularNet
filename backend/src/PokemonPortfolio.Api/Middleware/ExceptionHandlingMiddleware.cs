using System.Net;
using System.Text.Json;
using PokemonPortfolio.Domain.Exceptions;

namespace PokemonPortfolio.Api.Middleware;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await WriteErrorResponseAsync(context, exception);
        }
    }

    private static async Task WriteErrorResponseAsync(HttpContext context, Exception exception)
    {
        var statusCode = exception switch
        {
            NotFoundException => (int)HttpStatusCode.NotFound,
            ExternalServiceException => (int)HttpStatusCode.BadGateway,
            _ => (int)HttpStatusCode.InternalServerError
        };

        var payload = new
        {
            message = exception.Message,
            statusCode
        };

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
    }
}
