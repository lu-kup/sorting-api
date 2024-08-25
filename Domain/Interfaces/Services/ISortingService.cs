using Domain.Models.DTO;

namespace Domain.Interfaces.Services;

public interface ISortingService
{
    SortingOutputDTO Sort(SortingInputDTO input);
}
