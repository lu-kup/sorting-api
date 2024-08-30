namespace Domain.Models.DTO;

using Domain.Models.Enums;

public record SortingBaseDTO
{
    public required string NumberLine { get; init; }
    public required SortingAlgorithm SortingAlgorithm { get; init; } = default;
}
