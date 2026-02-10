using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexusMonitor.Api.Models
{    
    public class UserAccount
    {
        public int UserAccountId { get; set; }
        public required string? Username { get; set; }
        public required string? Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<Device>? Devices { get; set; } = [];
    }
    public class Device
    {
        public int DeviceId { get; set; }
        public string? DeviceName { get; set; }
        public string? SecretKey { get; set; }
        public DateOnly DateRegistered { get; set; }
        public List<Measurement>? Measurements { get; set; } = [];
        public string? MeasurementsUnit { get; set; }
        public string? DeviceType { get; set; }
        public float HighThreshold { get; set; }
        public float LowThreshold { get; set; }

    }

    public class Measurement    
    {
        public int MeasurementId { get; set; }
        public int DeviceId { get; set; }
        [ForeignKey("DeviceId")]
        public Device Device{ get; set; } = null!;
        public DateTime Timestamp { get; set; }
        public float Value { get; set; }
    }

}
