using Api.Dtos;
using FluentValidation;

namespace Api.Modules.Validators;

public class AboutDtoValidator : AbstractValidator<AboutDto>
{
    public AboutDtoValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MinimumLength(8).WithMessage("Description must be at least 8 characters long.")
            .MaximumLength(1000).WithMessage("Description must be at least 1000 characters long.");
    }
}