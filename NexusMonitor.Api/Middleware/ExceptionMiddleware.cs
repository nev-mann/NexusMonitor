using NexusMonitor.Api.Models;
using System.Net;

namespace NexusMonitor.Api.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;
        private readonly IHostEnvironment _env = env;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Wystąpił nieoczekiwany błąd: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = _env.IsDevelopment()
                ? new ErrorDetails
                {
                    StatusCode = context.Response.StatusCode,
                    Message = ex.Message,
                    Details = ex.StackTrace?.ToString()
                }
                : new ErrorDetails
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "Wystąpił błąd wewnętrzny serwera. Spróbuj ponownie później.",
                    Details = string.Empty
                };

            await context.Response.WriteAsync(response.ToString());
        }
    }
}