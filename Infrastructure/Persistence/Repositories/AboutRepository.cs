using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Abouts;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class AboutRepository(ApplicationDbContext context) : IAboutQueries, IAboutRepository
{
    public async Task<Option<About>> FirstOrDefault(CancellationToken cancellationToken)
    {
        var entity = await context.Abouts
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        return entity == null ? Option.None<About>() : Option.Some(entity);
    }

    public async Task<Option<About>> GetById(AboutId id, CancellationToken cancellationToken)
    {
        var entity = await context.Abouts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity == null ? Option.None<About>() : Option.Some(entity);
    }

    public async Task<About> Update(About about, CancellationToken cancellationToken)
    {
        context.Abouts.Update(about);

        await context.SaveChangesAsync(cancellationToken);

        return about;
    }
}