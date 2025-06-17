using Domain.ShortUrls;

namespace Api.Dtos;

public record ShortUrlDto(
    Guid? Id,
    string OriginalUrl,
    string? Slug,
    UserDto? User,
    DateTime? CreatedAt,
    DateTime? UpdatedAt)
{
    public static ShortUrlDto FromDomainModel(ShortUrl shortUrl)
        => new(
            Id: shortUrl.Id.Value,
            OriginalUrl: shortUrl.OriginalUrl,
            Slug: shortUrl.Slug,
            User: shortUrl.User == null ? null : UserDto.FromDomainModel(shortUrl.User),
            CreatedAt: shortUrl.CreatedAt,
            UpdatedAt: shortUrl.UpdatedAt);
}