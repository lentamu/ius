using Domain.Users;

namespace Api.Dtos;

public record UserDto(
    Guid? Id,
    string Username,
    DateTime? CreatedAt,
    DateTime? UpdatedAt)
{
    public static UserDto FromDomainModel(User user)
        => new(
            Id: user.Id.Value,
            Username: user.Username,
            CreatedAt: user.CreatedAt,
            UpdatedAt: user.UpdatedAt);
}

public record AuthUserDto(string Username, string Password);

public record AuthResponseDto(string Token);