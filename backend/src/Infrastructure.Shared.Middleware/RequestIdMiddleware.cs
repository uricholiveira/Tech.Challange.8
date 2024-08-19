using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Shared.Middleware;

public class RequestIdMiddleware
{
    private readonly ILogger<RequestIdMiddleware> _logger;
    private readonly RequestDelegate _next;

    public RequestIdMiddleware(RequestDelegate next, ILogger<RequestIdMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        httpContext.TraceIdentifier = Activity.Current?.TraceId.ToString() ?? httpContext.TraceIdentifier;

        await _next(httpContext);

        httpContext.Response.Headers.Add("X-Request-Id", httpContext.TraceIdentifier);
    }
}