using System.Text.Json;
using FluentValidation;
using Infrastructure.Shared.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Shared.Middleware;

public class ExceptionMiddleware(
    RequestDelegate next,
    ILogger<ExceptionMiddleware> logger,
    IHostEnvironment environment)
{
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (BadRequestException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(new { ex.Message, ex.Id }, _options);
            await context.Response.WriteAsync(json);
        }
        catch (NotFoundException ex)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            context.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(new { ex.Message, ex.Id }, _options);
            await context.Response.WriteAsync(json);
        }
        catch (ConflictException ex)
        {
            context.Response.StatusCode = StatusCodes.Status409Conflict;
            context.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(new { ex.Message, ex.Id }, _options);
            await context.Response.WriteAsync(json);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
            context.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(
                new
                {
                    Message = "Erro de validação",
                    Errors = ex.Errors.Select(x => new { Property = x.PropertyName, Message = x.ErrorMessage })
                }, _options);
            await context.Response.WriteAsync(json);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            logger.LogError(ex.Message, ex);

            if (!environment.IsProduction())
                throw;
        }
    }
}