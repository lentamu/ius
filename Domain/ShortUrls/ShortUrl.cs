using Domain.Users;

namespace Domain.ShortUrls;

public class ShortUrl
{
    public ShortUrlId Id { get; }

    public string OriginalUrl { get; private set; }
    public string Slug { get; private set; }

    public UserId UserId { get; }
    public User? User { get; }

    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private ShortUrl(
        ShortUrlId id,
        string originalUrl,
        string slug,
        UserId userId,
        DateTime createdAt,
        DateTime updatedAt)
    {
        Id = id;
        OriginalUrl = originalUrl;
        Slug = slug;
        UserId = userId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static ShortUrl New(ShortUrlId id, string originalUrl, string slug, UserId userId)
        => new(id, originalUrl, slug, userId, DateTime.UtcNow, DateTime.UtcNow);
}