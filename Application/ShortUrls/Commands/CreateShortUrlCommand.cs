using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.ShortUrls.Exceptions;
using Domain.ShortUrls;
using Domain.Users;
using MediatR;

namespace Application.ShortUrls.Commands;

public record CreateShortUrlCommand : IRequest<Result<ShortUrl, ShortUrlException>>
{
    public required string OriginalUrl { get; init; }
    public required Guid UserId { get; init; }
}

public class CreateShortUrlCommandHandler(
    IShortUrlRepository shortUrlRepository,
    ISlugGenerator slugGenerator)
    : IRequestHandler<CreateShortUrlCommand, Result<ShortUrl, ShortUrlException>>
{
    public async Task<Result<ShortUrl, ShortUrlException>> Handle(
        CreateShortUrlCommand request,
        CancellationToken cancellationToken)
    {
        var userId = new UserId(request.UserId);

        var existingUrl = await shortUrlRepository.GetByOriginalUrl(request.OriginalUrl, cancellationToken);

        return await existingUrl.Match(
            u => Task.FromResult<Result<ShortUrl, ShortUrlException>>(new ShortUrlAlreadyExistsException(u.Id)),
            async () =>
            {
                var slug = await slugGenerator.GenerateUniqueSlug(request.OriginalUrl, cancellationToken);

                return await CreateEntity(request.OriginalUrl, slug, userId, cancellationToken);
            });
    }

    private async Task<Result<ShortUrl, ShortUrlException>> CreateEntity(
        string originalUrl,
        string slug,
        UserId userId,
        CancellationToken cancellationToken)
    {
        try
        {
            var entity = ShortUrl.New(ShortUrlId.New(), originalUrl, slug, userId);

            return await shortUrlRepository.Add(entity, cancellationToken);
        }
        catch (Exception e)
        {
            return new ShortUrlUnknownException(ShortUrlId.Empty(), e);
        }
    }
}