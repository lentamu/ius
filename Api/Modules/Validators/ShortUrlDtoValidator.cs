using Api.Dtos;
using FluentValidation;

namespace Api.Modules.Validators;

public class ShortUrlDtoValidator : AbstractValidator<ShortUrlDto>
{
    public ShortUrlDtoValidator()
    {
        RuleFor(x => x.OriginalUrl)
            .NotEmpty().WithMessage("The original url is required.")
            .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute)).WithMessage("The original url is not a valid absolute URL.");
    }
}