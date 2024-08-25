namespace Domain.Models.DTO;

public record SortingOutputDTO : SortingBaseDTO
{
    public TimeSpan calculationTime { get; init; }
}
