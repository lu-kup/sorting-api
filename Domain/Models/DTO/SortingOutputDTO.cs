namespace Domain.Models.DTO;

using Domain.Models.Enums;

public record SortingOutputDTO : SortingBaseDTO
{
    public required TimeSpan CalculationTime { get; init; }
}
