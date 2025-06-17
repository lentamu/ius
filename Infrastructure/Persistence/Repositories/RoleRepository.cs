using Application.Common.Interfaces.Repositories;
using Domain.Roles;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class RoleRepository(ApplicationDbContext context) : IRoleRepository
{
    public async Task<Option<Role>> GetByName(string name, CancellationToken cancellationToken)
    {
        var entity = await context.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);

        return entity == null ? Option.None<Role>() : Option.Some(entity);
    }
}