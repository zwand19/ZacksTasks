using System.ComponentModel.DataAnnotations;

namespace OriginTest.Middleware;

public class LoggingMiddleware
{
  private readonly RequestDelegate _next;
  private readonly ILogger<LoggingMiddleware> _logger;

  public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
  {
    _next = next ?? throw new ArgumentNullException(nameof(next));
    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
  }

  public async Task Invoke(HttpContext httpContext)
  {
    if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

    try
    {
      await _next(httpContext);

      var statusCode = httpContext.Response.StatusCode;
      _logger.LogInformation("Request: {RequestMethod} {RequestPath} responded with {StatusCode}", httpContext.Request.Method,
        httpContext.Request.Path, statusCode);
    }
    // Never caught, because `LogException()` returns false.
    catch (Exception ex) when (LogException(httpContext, ex))
    {
    }
  }

  private bool LogException(HttpContext httpContext, Exception ex)
  {
    if (ex is ValidationException)
    {
      // todo: throw custom exceptions that store the status code that is shared with exception handling middleware so we don't have to
      // duplicate code/mapping of exceptions to status codes
      _logger.LogInformation("Request: {RequestMethod} {RequestPath} responded with {StatusCode}", httpContext.Request.Method,
        httpContext.Request.Path, 400);
    }
    else
    {
      _logger.LogError(ex, "An unhandled exception has occurred while executing the request.");
    }

    return false;
  }
}