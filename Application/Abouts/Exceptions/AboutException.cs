using Domain.Abouts;

namespace Application.Abouts.Exceptions;

public abstract class AboutException(AboutId id, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public AboutId AboutId { get; } = id;
}

public class AboutNotFoundException(AboutId id) : AboutException(id, $"About under id: {id} not found");

public class AboutUnknownException(AboutId id, Exception innerException)
    : AboutException(id, $"Unknown exception for the about under id: {id}", innerException);