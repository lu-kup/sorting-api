using Domain.Models.Enums;

namespace Domain.Models.DTO;

public record SortingBaseDTO
{
    public required string NumberLine { get; init; }
    public required SortingAlgorithm SortingAlgorithm { get; init; } = default;
}
