using Domain.Abouts;

namespace Api.Dtos;

public record AboutDto(
    Guid? Id,
    string Description,
    DateTime? CreatedAt,
    DateTime? UpdatedAt)
{
    public static AboutDto FromDomainModel(About about)
        => new(
            Id: about.Id.Value,
            Description: about.Description,
            CreatedAt: about.CreatedAt,
            UpdatedAt: about.UpdatedAt);
}