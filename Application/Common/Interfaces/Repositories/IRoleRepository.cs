using Domain.Roles;
using Optional;

namespace Application.Common.Interfaces.Repositories;

public interface IRoleRepository
{
    Task<Option<Role>> GetByName(string name, CancellationToken cancellationToken);
}