using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.ShortUrls;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class ShortUrlRepository(ApplicationDbContext context) : IShortUrlQueries, IShortUrlRepository
{
    public async Task<IReadOnlyList<ShortUrl>> GetAll(CancellationToken cancellationToken)
    {
        return await context.ShortUrls
            .AsNoTracking()
            .Include(x => x.User)
            .ToListAsync(cancellationToken);
    }

    public async Task<Option<ShortUrl>> GetById(ShortUrlId id, CancellationToken cancellationToken)
    {
        var entity = await context.ShortUrls
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity == null ? Option.None<ShortUrl>() : Option.Some(entity);
    }

    public async Task<Option<ShortUrl>> GetByOriginalUrl(string originalUrl, CancellationToken cancellationToken)
    {
        var entity = await context.ShortUrls
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.OriginalUrl == originalUrl, cancellationToken);

        return entity == null ? Option.None<ShortUrl>() : Option.Some(entity);
    }

    public async Task<Option<ShortUrl>> GetBySlug(string slug, CancellationToken cancellationToken)
    {
        var entity = await context.ShortUrls
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Slug == slug, cancellationToken);

        return entity == null ? Option.None<ShortUrl>() : Option.Some(entity);
    }

    public async Task<Option<ShortUrl>> GetByShortUrlAndUserId(ShortUrlId urlId, UserId userId,
        CancellationToken cancellationToken)
    {
        var entity = await context.ShortUrls
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == urlId && x.UserId == userId, cancellationToken);

        return entity == null ? Option.None<ShortUrl>() : Option.Some(entity);
    }

    public async Task<ShortUrl> Add(ShortUrl shortUrl, CancellationToken cancellationToken)
    {
        await context.ShortUrls.AddAsync(shortUrl, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return shortUrl;
    }

    public async Task<ShortUrl> Update(ShortUrl shortUrl, CancellationToken cancellationToken)
    {
        context.ShortUrls.Update(shortUrl);

        await context.SaveChangesAsync(cancellationToken);

        return shortUrl;
    }

    public async Task<ShortUrl> Delete(ShortUrl shortUrl, CancellationToken cancellationToken)
    {
        context.ShortUrls.Remove(shortUrl);

        await context.SaveChangesAsync(cancellationToken);

        return shortUrl;
    }
}