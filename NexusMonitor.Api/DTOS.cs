using System.ComponentModel.DataAnnotations;

namespace NexusMonitor.Api.Dtos
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
        public string? DeviceName { get; set; }
    }

    public class UpdateDeviceDto{
        [StringLength(100, ErrorMessage = "Nazwa jest za długa (max 100 znaków).")]
        [Required(ErrorMessage = "Nazwa jest wymagana!")]
        public string? DeviceName { get; set; }
        public string? MeasurementsUnit { get; set; }
        public string? DeviceType { get; set; }
        public float HighThreshold { get; set; }
        public float LowThreshold { get; set; }
    }

    public class CreateMeasurementDto
    {
        [Required] public string? DeviceId { get; set; }
        [Required] public double Value { get; set; }
    }

}
