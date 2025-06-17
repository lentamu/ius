using Application.Users.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Errors;

public static class UserErrorHandler
{
    public static ObjectResult ToObjectResult(this UserException exception)
    {
        return new ObjectResult(new { message = exception.Message })
        {
            StatusCode = exception switch
            {
                UserInvalidCredentialsException => StatusCodes.Status401Unauthorized,
                UserNotFoundException => StatusCodes.Status404NotFound,
                UserRoleNotFoundException => StatusCodes.Status404NotFound,
                UserAlreadyExistsException => StatusCodes.Status409Conflict,
                UserUnknownException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("User error handler does not implemented")
            }
        };
    }
}