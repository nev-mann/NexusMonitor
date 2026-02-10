using NexusMonitor.Api.Models;
using System.Net;

namespace NexusMonitor.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env; // Pozwala sprawdzić, czy jesteśmy w Development czy Production
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Przekazujemy żądanie dalej do kolejnych middleware'ów (np. do Kontrolera)
                await _next(context);
            }
            catch (Exception ex)
            {
                // Jeśli gdziekolwiek "głębiej" wystąpi błąd, łapiemy go tutaj
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
                    Details = null
                };

            await context.Response.WriteAsync(response.ToString());
        }
    }
}