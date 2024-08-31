namespace Domain.Models.DTO;

using Domain.Models.Enums;

public record SortingResultDTO
{
    public required int[] SortedArray { get; init; }
    public required SortingAlgorithm SortingAlgorithm { get; init; }
    public TimeSpan CalculationTime { get; init; }
}
