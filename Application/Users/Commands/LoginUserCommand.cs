using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Users.Exceptions;
using MediatR;

namespace Application.Users.Commands;

public record LoginUserCommand : IRequest<Result<string, UserException>>
{
    public required string Username { get; init; }
    public required string Password { get; init; }
}

public class LoginUserCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    ITokenGenerator tokenGenerator) : IRequestHandler<LoginUserCommand, Result<string, UserException>>
{
    public async Task<Result<string, UserException>> Handle(
        LoginUserCommand request,
        CancellationToken cancellationToken)
    {
        var existingUser = await userRepository.GetByUsername(request.Username, cancellationToken);

        return await existingUser.Match(
            async user =>
            {
                if (!passwordHasher.Verify(user.Password, request.Password))
                {
                    return new UserInvalidCredentialsException();
                }

                return tokenGenerator.GenerateToken(user);
            },
            () => Task.FromResult<Result<string, UserException>>(new UserInvalidCredentialsException()));
    }
}