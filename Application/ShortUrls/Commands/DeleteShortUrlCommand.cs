using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.ShortUrls.Exceptions;
using Domain.ShortUrls;
using Domain.Users;
using MediatR;

namespace Application.ShortUrls.Commands;

public record DeleteShortUrlCommand : IRequest<Result<ShortUrl, ShortUrlException>>
{
    public required Guid ShortUrlId { get; init; }
    public required Guid UserId { get; init; }
}

public class DeleteShortUrlCommandHandler(
    IShortUrlRepository shortUrlRepository,
    IUserRepository userRepository)
    : IRequestHandler<DeleteShortUrlCommand, Result<ShortUrl, ShortUrlException>>
{
    public async Task<Result<ShortUrl, ShortUrlException>> Handle(
        DeleteShortUrlCommand request,
        CancellationToken cancellationToken)
    {
        var shortUrlId = new ShortUrlId(request.ShortUrlId);
        var userId = new UserId(request.UserId);

        var existingUser = await userRepository.GetById(userId, cancellationToken);

        return await existingUser.Match(
            async user =>
            {
                if (user.IsAdmin())
                {
                    var existingUrl = await shortUrlRepository.GetById(shortUrlId, cancellationToken);
                    return await existingUrl.Match(
                        async url => await DeleteEntity(url, cancellationToken),
                        () => Task.FromResult<Result<ShortUrl, ShortUrlException>>(
                            new ShortUrlNotFoundException(shortUrlId))
                    );
                }
                else
                {
                    var existingUrl = await shortUrlRepository.GetByShortUrlAndUserId(shortUrlId, userId, cancellationToken);
                    return await existingUrl.Match(
                        async url => await DeleteEntity(url, cancellationToken),
                        () => Task.FromResult<Result<ShortUrl, ShortUrlException>>(
                            new ShortUrlAccessDeniedException(shortUrlId))
                    );
                }
            },
            () => Task.FromResult<Result<ShortUrl, ShortUrlException>>(new ShortUrlAccessDeniedException(shortUrlId)));
    }

    private async Task<Result<ShortUrl, ShortUrlException>> DeleteEntity(
        ShortUrl entity,
        CancellationToken cancellationToken)
    {
        try
        {
            return await shortUrlRepository.Delete(entity, cancellationToken);
        }
        catch (Exception e)
        {
            return new ShortUrlUnknownException(ShortUrlId.Empty(), e);
        }
    }
}