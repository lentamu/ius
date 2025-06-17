using Domain.ShortUrls;

namespace Application.ShortUrls.Exceptions;

public abstract class ShortUrlException(ShortUrlId id, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public ShortUrlId ShortUrlId { get; } = id;
}

public class ShortUrlNotFoundException(ShortUrlId id) : ShortUrlException(id, $"Short url under id: {id} not found");

public class ShortUrlAlreadyExistsException(ShortUrlId id) : ShortUrlException(id, $"Short url already exists: {id}");

public class ShortUrlAccessDeniedException(ShortUrlId id) : ShortUrlException(id, $"Short url under id: {id} access denied");

public class ShortUrlUnknownException(ShortUrlId id, Exception innerException)
    : ShortUrlException(id, $"Unknown exception for the short url under id: {id}", innerException);