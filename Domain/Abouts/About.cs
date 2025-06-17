namespace Domain.Abouts;

public class About
{
    public AboutId Id { get; }

    public string Description { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private About(AboutId id, string description, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        Description = description;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static About New(AboutId id, string description)
        => new(id, description, DateTime.UtcNow, DateTime.UtcNow);

    public void UpdateDetails(string description)
    {
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }
}