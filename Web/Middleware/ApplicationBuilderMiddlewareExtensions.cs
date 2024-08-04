namespace OriginTest.Middleware;

public static class ApplicationBuilderMiddlewareExtensions
{
  public static void UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
  {
    builder.UseMiddleware<ExceptionHandlingMiddleware>();
  }
  
  public static void UseLoggingMiddleware(this IApplicationBuilder builder)
  {
    builder.UseMiddleware<LoggingMiddleware>();
  }
}