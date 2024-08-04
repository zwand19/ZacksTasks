using System.ComponentModel.DataAnnotations;

namespace OriginTest.Middleware;

public class ExceptionHandlingMiddleware
{
  private readonly RequestDelegate _next;

  public ExceptionHandlingMiddleware(RequestDelegate next)
  {
    _next = next ?? throw new ArgumentNullException(nameof(next));
  }

  public async Task Invoke(HttpContext context)
  {
    try
    {
      await _next(context);
    }
    catch (ValidationException ex)
    {
      context.Response.Clear();
      context.Response.StatusCode = 400;
      context.Response.ContentType = "application/json";

      await context.Response.WriteAsync(ex.Message);
    }
  }
}