namespace LibrarySimulator.Middleware;

public class BrowserNotSupportedMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        string? userAgent = context.Request.Headers.UserAgent;

        if (IsInternetExplorer(userAgent))
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync("Browser not supported.");
            return;
        }

        await next(context);
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
