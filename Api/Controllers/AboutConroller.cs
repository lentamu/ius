using Api.Dtos;
using Api.Modules.Errors;
using Application.Abouts.Commands;
using Application.Common.Interfaces.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("about")]
[ApiController]
public class AboutController(ISender sender, IAboutQueries aboutQueries) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<AboutDto>> Get(CancellationToken cancellationToken)
    {
        var entity = await aboutQueries.FirstOrDefault(cancellationToken);

        return entity.Match<ActionResult<AboutDto>>(
            a => AboutDto.FromDomainModel(a),
            () => NotFound());
    }

    [HttpPatch]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<AboutDto>> Update(
        [FromBody] AboutDto request,
        CancellationToken cancellationToken)
    {
        var input = new UpdateAboutCommand
        {
            AboutId = request.Id!.Value,
            Description = request.Description
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<AboutDto>>(
            f => AboutDto.FromDomainModel(f),
            e => e.ToObjectResult());
    }
}