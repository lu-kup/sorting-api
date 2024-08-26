using Domain.Interfaces.Services;
using Domain.Models.DTO;
using Domain.Models.Enums;

namespace Application.Services;

public class SortingService : ISortingService
{
    public SortingOutputDTO Sort(int[] inputArray, SortingAlgorithm sortingAlgorithm) =>
        sortingAlgorithm switch 
        {
            SortingAlgorithm.SelectionSort =>  SelectionSort(inputArray),
            _ => throw new InvalidOperationException()
        };

    private SortingOutputDTO SelectionSort(int[] array)
    {
        throw new NotImplementedException();
    }
}
