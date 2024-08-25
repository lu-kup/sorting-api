using Domain.Interfaces.Services;
using Domain.Models.DTO;
using Domain.Models.Enums;

namespace Application.Services;

public class SortingService : ISortingService
{
    public SortingOutputDTO Sort(SortingInputDTO sortingInputDTO) =>
        sortingInputDTO.SortingAlgorithm switch 
        {
            SortingAlgorithm.SelectionSort =>  SelectionSort(sortingInputDTO.NumberLine),
            _ => throw new InvalidOperationException()
        };

    private SortingOutputDTO SelectionSort(int[] array)
    {
        throw new NotImplementedException();
    }
}
