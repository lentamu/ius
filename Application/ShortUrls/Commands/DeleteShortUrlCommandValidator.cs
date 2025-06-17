using FluentValidation;

namespace Application.ShortUrls.Commands;

public class DeleteShortUrlCommandValidator : AbstractValidator<DeleteShortUrlCommand>
{
    public DeleteShortUrlCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User id is required.");
        
        RuleFor(x => x.ShortUrlId)
            .NotEmpty().WithMessage("Short url id is required.");
    }
}