using Api.Dtos;
using Api.Modules.Errors;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Queries;
using Application.ShortUrls.Commands;
using Domain.ShortUrls;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("short-urls")]
[ApiController]
public class ShortUrlsController(
    ISender sender,
    IShortUrlQueries shortUrlQueries,
    IJwtDecoder jwtDecoder) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ShortUrlDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var entities = await shortUrlQueries.GetAll(cancellationToken);

        return entities.Select(ShortUrlDto.FromDomainModel).ToList();
    }

    [HttpGet("{shorturlId:guid}")]
    [Authorize]
    public async Task<ActionResult<ShortUrlDto>> GetById(
        [FromRoute] Guid shorturlId,
        CancellationToken cancellationToken)
    {
        var entity = await shortUrlQueries.GetById(new ShortUrlId(shorturlId), cancellationToken);

        return entity.Match<ActionResult<ShortUrlDto>>(
            u => ShortUrlDto.FromDomainModel(u),
            () => NotFound());
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ShortUrlDto>> Create(
        [FromBody] ShortUrlDto request,
        CancellationToken cancellationToken)
    {
        var userId = Request.GetUserIdFromToken(jwtDecoder);

        var input = new CreateShortUrlCommand
        {
            OriginalUrl = request.OriginalUrl,
            UserId = userId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ShortUrlDto>>(
            f => ShortUrlDto.FromDomainModel(f),
            e => e.ToObjectResult());
    }

    [HttpDelete("{shorturlId:guid}")]
    [Authorize]
    public async Task<ActionResult<ShortUrlDto>> Delete(
        [FromRoute] Guid shorturlId,
        CancellationToken cancellationToken)
    {
        var userId = Request.GetUserIdFromToken(jwtDecoder);

        var input = new DeleteShortUrlCommand
        {
            ShortUrlId = shorturlId,
            UserId = userId
        };

        var result = await sender.Send(input, cancellationToken);

        return result.Match<ActionResult<ShortUrlDto>>(
            u => ShortUrlDto.FromDomainModel(u),
            e => e.ToObjectResult());
    }

    [HttpGet("/r/{slug}")]
    public async Task<ActionResult> Redirect(
        [FromRoute] string slug,
        CancellationToken cancellationToken)
    {
        var entity = await shortUrlQueries.GetBySlug(slug, cancellationToken);

        return entity.Match<ActionResult>(
            u => Redirect(u.OriginalUrl),
            () => NotFound());
    }
}