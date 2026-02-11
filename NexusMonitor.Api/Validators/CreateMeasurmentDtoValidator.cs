using FluentValidation;
using NexusMonitor.Api.Models;

namespace NexusMonitor.Api.Validators 
{
    public class CreateMeasurmentDtoValidator : AbstractValidator<CreateMeasurementDto>
    {
        public CreateMeasurmentDtoValidator() 
        {
            RuleFor(x => x.DeviceId)
                .NotEmpty().WithMessage("DeviceId jest wymagany!")
                .MaximumLength(100).WithMessage("DeviceId jest za długi (max 100 znaków).");
            RuleFor(x => x.Value)
                .NotEmpty().WithMessage("Value jest wymagany!");
        }
    }
}
