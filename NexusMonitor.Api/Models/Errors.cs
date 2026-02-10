using System.Text.Json;

namespace NexusMonitor.Api.Models
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Details { get; set; } // Stack trace

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}