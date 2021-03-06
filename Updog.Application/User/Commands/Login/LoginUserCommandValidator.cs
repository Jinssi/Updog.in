using FluentValidation;
using Updog.Application;

namespace Updog.Application {
    public sealed class LoginUserCommandValidator : FluentValidatorAdapter<LoginUserCommand> {
        public LoginUserCommandValidator() {
            RuleFor(c => c.Credentials.Username).NotNull().WithMessage("Username is required.");
            RuleFor(c => c.Credentials.Username).NotEmpty().WithMessage("Username is required.");

            RuleFor(c => c.Credentials.Password).NotNull().WithMessage("Password is required.");
            RuleFor(c => c.Credentials.Password).NotEmpty().WithMessage("Password is required.");
        }
    }
}