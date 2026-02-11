using FluentValidation;
using NexusMonitor.Api.Models;

namespace NexusMonitor.Api.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator() 
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email jest wymagany!")
                .EmailAddress().WithMessage("Nieprawidłowy format email.");
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Nazwa użytkownika jest wymagana!")
                .MinimumLength(3).WithMessage("Nazwa użytkownika musi mieć co najmniej 3 znaki.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Hasło jest wymagane!")
                .MinimumLength(8).WithMessage("Hasło musi mieć co najmniej 8 znaków.");
        }
    }
}
