using FluentValidation;

namespace Application.ShortUrls.Commands;

public class CreateShortUrlCommandValidator : AbstractValidator<CreateShortUrlCommand>
{
    public CreateShortUrlCommandValidator()
    {
        RuleFor(x => x.OriginalUrl)
            .NotEmpty().WithMessage("The original url is required.");
    }
}