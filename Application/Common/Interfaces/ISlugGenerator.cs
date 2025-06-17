namespace Application.Common.Interfaces;

public interface ISlugGenerator
{
    Task<string> GenerateUniqueSlug(string originalUrl, CancellationToken cancellationToken);
}