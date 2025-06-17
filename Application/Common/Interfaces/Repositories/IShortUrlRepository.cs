using Domain.ShortUrls;
using Domain.Users;
using Optional;

namespace Application.Common.Interfaces.Repositories;

public interface IShortUrlRepository
{
    Task<Option<ShortUrl>> GetById(ShortUrlId id, CancellationToken cancellationToken);
    Task<Option<ShortUrl>> GetByOriginalUrl(string originalUrl, CancellationToken cancellationToken);
    Task<Option<ShortUrl>> GetBySlug(string slug, CancellationToken cancellationToken);
    Task<Option<ShortUrl>> GetByShortUrlAndUserId(ShortUrlId urlId, UserId userId, CancellationToken cancellationToken);

    Task<ShortUrl> Add(ShortUrl shortUrl, CancellationToken cancellationToken);
    Task<ShortUrl> Update(ShortUrl shortUrl, CancellationToken cancellationToken);
    Task<ShortUrl> Delete(ShortUrl shortUrl, CancellationToken cancellationToken);
}