namespace Domain.Models.DTO;

using Domain.Models.Enums;

public abstract record SortingBaseDTO
{
    public required int[] NumberLine { get; init; }
    public required SortingAlgorithm SortingAlgorithm { get; init; } = default;
}
