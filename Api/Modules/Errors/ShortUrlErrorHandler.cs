using Application.ShortUrls.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Errors;

public static class ShortUrlErrorHandler
{
    public static ObjectResult ToObjectResult(this ShortUrlException exception)
    {
        return new ObjectResult(new { message = exception.Message })
        {
            StatusCode = exception switch
            {
                ShortUrlAccessDeniedException => StatusCodes.Status403Forbidden,
                ShortUrlNotFoundException => StatusCodes.Status404NotFound,
                ShortUrlAlreadyExistsException => StatusCodes.Status409Conflict,
                ShortUrlUnknownException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("Short url error handler does not implemented")
            }
        };
    }
}