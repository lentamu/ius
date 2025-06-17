using Application.Abouts.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Errors;

public static class AboutErrorHandler
{
    public static ObjectResult ToObjectResult(this AboutException exception)
    {
        return new ObjectResult(new { message = exception.Message })
        {
            StatusCode = exception switch
            {
                AboutNotFoundException => StatusCodes.Status404NotFound,
                AboutUnknownException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("About error handler does not implemented")
            }
        };
    }
}