using Application.Common.Interfaces;

namespace Infrastructure.Persistence.Converters;

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string raw)
    {
        return BCrypt.Net.BCrypt.HashPassword(raw);
    }

    public bool Verify(string hash, string raw)
    {
        return BCrypt.Net.BCrypt.Verify(raw, hash);
    }
}