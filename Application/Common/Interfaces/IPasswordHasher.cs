namespace Application.Common.Interfaces;

public interface IPasswordHasher
{
    public string Hash(string raw);
    public bool Verify(string hash, string raw);
}