using Domain.Abouts;
using Optional;

namespace Application.Common.Interfaces.Queries;

public interface IAboutQueries
{
    Task<Option<About>> FirstOrDefault(CancellationToken cancellationToken);
}