using Domain.Models.DTO;
using Domain.Models.Enums;

namespace Domain.Interfaces.Services;

public interface ISortingService
{
    SortingOutputDTO Sort(int[] array, SortingAlgorithm sortingAlgorithm);
}
