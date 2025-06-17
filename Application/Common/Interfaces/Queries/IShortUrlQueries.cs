using Domain.ShortUrls;
using Optional;

namespace Application.Common.Interfaces.Queries;

public interface IShortUrlQueries
{
    Task<IReadOnlyList<ShortUrl>> GetAll(CancellationToken cancellationToken);
    Task<Option<ShortUrl>> GetById(ShortUrlId id, CancellationToken cancellationToken);
    Task<Option<ShortUrl>> GetByOriginalUrl(string originalUrl, CancellationToken cancellationToken);
    Task<Option<ShortUrl>> GetBySlug(string slug, CancellationToken cancellationToken);
}