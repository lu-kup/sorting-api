using Domain.Models.DTO;

namespace Domain.Interfaces.Services;

public interface IDataProcessingService
{
    Task<SortingOutputDTO> SortAsync(SortingInputDTO sortingInputDTO);
    Task<int[]> GetLatestAsync();
}
