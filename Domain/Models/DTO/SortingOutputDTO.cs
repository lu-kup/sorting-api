namespace Domain.Models.DTO;

using Domain.Models.Enums;

public record SortingOutputDTO : SortingBaseDTO
{
    public TimeSpan CalculationTime { get; init; }
}
