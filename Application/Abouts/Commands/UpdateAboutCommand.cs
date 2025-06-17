using Application.Abouts.Exceptions;
using Application.Common;
using Application.Common.Interfaces.Repositories;
using Domain.Abouts;
using MediatR;

namespace Application.Abouts.Commands;

public record UpdateAboutCommand : IRequest<Result<About, AboutException>>
{
    public required Guid AboutId { get; init; }
    public required string Description { get; init; }
}

public class UpdateAboutCommandHandler(
    IAboutRepository aboutRepository)
    : IRequestHandler<UpdateAboutCommand, Result<About, AboutException>>
{
    public async Task<Result<About, AboutException>> Handle(UpdateAboutCommand request,
        CancellationToken cancellationToken)
    {
        var aboutId = new AboutId(request.AboutId);
        var existingAbout = await aboutRepository.GetById(aboutId, cancellationToken);

        return await existingAbout.Match(
            async a => await UpdateEntity(a, request.Description, cancellationToken),
            () => Task.FromResult<Result<About, AboutException>>(new AboutNotFoundException(aboutId)));
    }

    private async Task<Result<About, AboutException>> UpdateEntity(
        About entity,
        string description,
        CancellationToken cancellationToken)
    {
        try
        {
            entity.UpdateDetails(description);

            return await aboutRepository.Update(entity, cancellationToken);
        }
        catch (Exception e)
        {
            return new AboutUnknownException(AboutId.Empty(), e);
        }
    }
}