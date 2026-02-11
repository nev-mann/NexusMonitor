using FluentValidation;
using NexusMonitor.Api.Models;

namespace NexusMonitor.Api.Validators
{
    public class CreateDeviceDtoValidator : AbstractValidator<CreateDeviceDto>
    {
        public CreateDeviceDtoValidator() 
        { 
            RuleFor(x => x.DeviceName)
                .NotEmpty().WithMessage("Nazwa jest wymagana!")
                .MaximumLength(100).WithMessage("Nazwa jest za długa (max 100 znaków).");
        }
    }
}
