using System.ComponentModel.DataAnnotations;

namespace NexusMonitor.Api.Models
{
    public record RegisterUserDto(
        [Required][EmailAddress] string Email,
        [Required][MinLength(3)] string Username,
        [Required][MinLength(8)] string Password
    );
    public class CreateDeviceDto
    {
        [StringLength(100, ErrorMessage = "Nazwa jest za długa (max 100 znaków).")]
        [Required(ErrorMessage = "Nazwa jest wymagana!")]
        public string DeviceName { get; set; } = string.Empty;
    }

    public class UpdateDeviceDto{
        [StringLength(100, ErrorMessage = "Nazwa jest za długa (max 100 znaków).")]
        [Required(ErrorMessage = "Nazwa jest wymagana!")]
        public string DeviceName { get; set; } = string.Empty;
        public string MeasurementsUnit { get; set; } = string.Empty;
        public string DeviceType { get; set; } = string.Empty;
        public float HighThreshold { get; set; }
        public float LowThreshold { get; set; }
    }

    public class DeviceDto
    {
        public int DeviceId { get; set; }
        public string DeviceName { get; set; } = string.Empty;
        public DateOnly DateRegistered { get; set; }
        public List<MeasurementDto> Measurements { get; set; } = [];
        public string MeasurementsUnit { get; set; } = string.Empty;
        public string DeviceType { get; set; } = string.Empty;
        public float HighThreshold { get; set; }
        public float LowThreshold { get; set; }
    }

    public class CreateMeasurementDto
    {
        [Required] public string DeviceId { get; set; } = string.Empty;
        [Required] public double Value { get; set; }
    }

    public class MeasurementDto
    {
        public int MeasurementId { get; set; }
        public int DeviceId { get; set; }
        public DateTime Timestamp { get; set; }
        public float Value { get; set; }
    }

}
