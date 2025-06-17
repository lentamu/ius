using Api.Dtos;
using Api.Modules.Errors;
using Application.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("")]
[ApiController]
public class AuthController(ISender sender) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(
        [FromBody] AuthUserDto request,
        CancellationToken cancellationToken)
    {
        var input = new CreateUserCommand
        {
            Username = request.Username,
            Password = request.Password,
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<UserDto>>(
            u => UserDto.FromDomainModel(u),
            e => e.ToObjectResult());
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(
        [FromBody] AuthUserDto request,
        CancellationToken cancellationToken)
    {
        var input = new LoginUserCommand
        {
            Username = request.Username,
            Password = request.Password
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<AuthResponseDto>>(
            t => new AuthResponseDto(t),
            e => e.ToObjectResult());
    }
}