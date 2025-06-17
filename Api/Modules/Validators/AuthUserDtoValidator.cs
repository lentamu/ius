using Api.Dtos;
using FluentValidation;

namespace Api.Modules.Validators;

public class AuthUserDtoValidator : AbstractValidator<AuthUserDto>
{
    public AuthUserDtoValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
            .MaximumLength(20).WithMessage("Username must be at least 20 characters long.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .MaximumLength(40).WithMessage("Password must be at least 40 characters long.");
    }
}