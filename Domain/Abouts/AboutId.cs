namespace Domain.Abouts;

public record AboutId(Guid Value)
{
    public static AboutId New() => new(Guid.NewGuid());
    public static AboutId Empty() => new(Guid.Empty);
    public override string ToString() => Value.ToString();
}