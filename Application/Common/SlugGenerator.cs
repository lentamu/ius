using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;

namespace Application.Common;

public class SlugGenerator(IShortUrlRepository shortUrlRepository) : ISlugGenerator
{
    private readonly int _slugLength = 40;
    
    public async Task<string> GenerateUniqueSlug(string originalUrl, CancellationToken cancellationToken)
    {
        const string alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
        var random = new Random();

        while (true)
        {
            var len = originalUrl.Length > _slugLength ? _slugLength :  originalUrl.Length;
            
            var slug = new string(Enumerable.Range(0, len)
                .Select(_ => alphabet[random.Next(alphabet.Length)])
                .ToArray());

            var existing = await shortUrlRepository.GetBySlug(slug, cancellationToken);

            if (!existing.HasValue)
                return slug;
        }
    }
}