using Domain.Roles;

namespace Domain.Users;

public class User
{
    public UserId Id { get; }

    public string Username { get; private set; }
    public string Password { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public RoleId RoleId { get; private set; }
    public Role? Role { get; }

    private User(
        UserId id,
        string username,
        string password,
        DateTime createdAt,
        DateTime updatedAt,
        RoleId roleId)
    {
        Id = id;
        Username = username;
        Password = password;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        RoleId = roleId;
    }

    public static User New(UserId id, string username, string password, RoleId roleId)
        => new(id, username, password, DateTime.UtcNow, DateTime.UtcNow, roleId);

    public bool IsAdmin() => Role?.Name == "Admin";
}