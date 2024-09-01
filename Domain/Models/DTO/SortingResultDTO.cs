using Domain.Models.Enums;

namespace Domain.Models.DTO;

public record SortingResultDTO
{
    public required int[] SortedArray { get; init; }
    public required SortingAlgorithm SortingAlgorithm { get; init; }
    public TimeSpan CalculationTime { get; init; }
}
