using Domain.Models.DTO;

namespace Domain.Interfaces.Services;

public interface IRequestProcessingService
{
    Task<SortingOutputDTO> SortAsync(SortingInputDTO sortingInputDTO);
    Task<IEnumerable<SortingOutputDTO>> SortAllAlgorithmsAsync(string numberLine);
    Task<string> GetLatestAsync();
}
