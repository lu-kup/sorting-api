using Domain.Models.DTO;

namespace Domain.Interfaces.Services;

public interface IRequestProcessingService
{
    Task<SortingOutputDTO> SortAsync(SortingInputDTO sortingInputDTO);
    Task<string> GetLatestAsync();
}
