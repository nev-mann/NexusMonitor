using System.ComponentModel.DataAnnotations;

namespace NexusMonitor.Api
{    
    public class UserAccount
    {
        public required Guid UserId { get; set; }
        public required string? Username { get; set; }
        public required string? Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<Device>? Devices { get; set; } = [];
    }
    public record RegisterUserDto(
        [Required][EmailAddress] string Email,
        [Required][MinLength(3)] string Username,
        [Required][MinLength(8)] string Password
    );
    public class Device
    {
        public string? DeviceId { get; set; }
        public string? DeviceName { get; set; }
        private string? SecretKey { get; set; }
        public DateOnly DateRegistered { get; set; }
    }

    public class Measurement    
    {
        public int MeasurementId { get; set; }
        public required string? DeviceId { get; set; }
        public DateTime Timestamp { get; set; }
        public double Value { get; set; }
    }

}
