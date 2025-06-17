using FluentValidation;

namespace Application.Users.Commands;

public class LoginUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}