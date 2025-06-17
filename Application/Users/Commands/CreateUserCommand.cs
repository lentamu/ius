using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Users.Exceptions;
using Domain.Roles;
using Domain.Users;
using MediatR;

namespace Application.Users.Commands;

public record CreateUserCommand : IRequest<Result<User, UserException>>
{
    public required string Username { get; init; }
    public required string Password { get; init; }
}

public class CreateUserCommandHandler(
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    IPasswordHasher passwordHasher) : IRequestHandler<CreateUserCommand, Result<User, UserException>>
{
    private readonly string _defaultRoleName = "User";

    public async Task<Result<User, UserException>> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        var role = await roleRepository.GetByName(_defaultRoleName, cancellationToken);

        return await role.Match(
            async r =>
            {
                var existingUser = await userRepository.GetByUsername(request.Username, cancellationToken);

                return await existingUser.Match(u =>
                        Task.FromResult<Result<User, UserException>>(new UserAlreadyExistsException(u.Id)),
                    async () => await CreateEntity(request.Username, request.Password, r, cancellationToken));
            },
            () => Task.FromResult<Result<User, UserException>>(new UserRoleNotFoundException(_defaultRoleName)));
    }

    private async Task<Result<User, UserException>> CreateEntity(
        string username,
        string password,
        Role role,
        CancellationToken cancellationToken)
    {
        try
        {
            var hashedPassword = passwordHasher.Hash(password);

            var entity = User.New(UserId.New(), username, hashedPassword, role.Id);

            return await userRepository.Add(entity, cancellationToken);
        }
        catch (Exception e)
        {
            return new UserUnknownException(UserId.Empty(), e);
        }
    }
}