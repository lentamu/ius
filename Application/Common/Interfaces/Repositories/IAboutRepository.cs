using Domain.Abouts;
using Optional;

namespace Application.Common.Interfaces.Repositories;

public interface IAboutRepository
{
    Task<Option<About>> GetById(AboutId id, CancellationToken cancellationToken);

    Task<About> Update(About about, CancellationToken cancellationToken);
}