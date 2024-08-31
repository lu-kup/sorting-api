namespace Domain.Models.DTO;

public record SortingOutputDTO : SortingBaseDTO
{
    public required string CalculationTimeInMilliseconds { get; init; }
}
