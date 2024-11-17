namespace LibrarySimulator.Middleware;

public class BrowserNotSupportedMiddleware
{
    private readonly RequestDelegate _next;

    public BrowserNotSupportedMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string? userAgent = context.Request.Headers.UserAgent;

        if (IsInternetExplorer(userAgent))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Browser not supported.");
            return;
        }

        await _next(context);
    }

    private static bool IsInternetExplorer(string? userAgent)
    {
        if (string.IsNullOrWhiteSpace(userAgent))
        {
            return false;
        }

        return userAgent.Contains("MSIE") || userAgent.Contains("Trident");
    }
}

public static class MyCustomMiddlewareExtensions
{
    public static IApplicationBuilder UseBrowserNotSupportedMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<BrowserNotSupportedMiddleware>();
    }
}
