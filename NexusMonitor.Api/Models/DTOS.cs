using System.ComponentModel.DataAnnotations;

namespace NexusMonitor.Api.Models
{
    public record RegisterUserDto(
        string Email,
        string Username,
        string Password
    );
    public record CreateDeviceDto
    (
         string DeviceName
    );

    public record UpdateDeviceDto(
        string? DeviceName,
        string? MeasurementsUnit,
        string? DeviceType,
        float HighThreshold,
        float LowThreshold
    );

    public record DeviceDto
    (
        int DeviceId,
        string? DeviceName,
        DateOnly DateRegistered,
        List<MeasurementDto> Measurements,
        string? MeasurementsUnit,
        string? DeviceType,
        float HighThreshold,
        float LowThreshold
    );

    public record CreateMeasurementDto
    (
        string DeviceId,
        double Value
    );

    public record MeasurementDto
    (
        int MeasurementId,
        int DeviceId,
        DateTime Timestamp,
        float Value
    );

}
