using FluentValidation;
using NexusMonitor.Api.Models;

namespace NexusMonitor.Api.Validators
{
    public class UpdateDeviceDtoValidator : AbstractValidator<UpdateDeviceDto>
    {
        public UpdateDeviceDtoValidator()
        {
            RuleFor(x => x.DeviceName)
                .NotEmpty().WithMessage("Nazwa jest wymagana!")
                .MaximumLength(100).WithMessage("Nazwa jest za długa (max 100 znaków).");
            RuleFor(x => x.MeasurementsUnit)
                .MaximumLength(10).WithMessage("Jednostka pomiaru jest za długa (max 10 znaków).");
            RuleFor(x => x.DeviceType)
                .MaximumLength(100).WithMessage("Typ urządzenia jest za długi (max 100 znaków).");
        }
    }
}
