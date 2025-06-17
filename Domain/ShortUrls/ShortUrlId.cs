namespace Domain.ShortUrls;

public record ShortUrlId(Guid Value)
{
    public static ShortUrlId New() => new(Guid.NewGuid());
    public static ShortUrlId Empty() => new(Guid.Empty);
    public override string ToString() => Value.ToString();
}