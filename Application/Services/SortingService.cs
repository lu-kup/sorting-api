using Domain.Interfaces.Services;
using Domain.Models.DTO;
using Domain.Models.Enums;

namespace Application.Services;

public class SortingService : ISortingService
{
    public SortingResultDTO Sort(int[] inputArray, SortingAlgorithm sortingAlgorithm) =>
        sortingAlgorithm switch 
        {
            SortingAlgorithm.BubbleSort => BubbleSort(inputArray),
            SortingAlgorithm.SelectionSort =>  SelectionSort(inputArray),
            _ => throw new InvalidOperationException()
        };

    private SortingResultDTO BubbleSort(int[] array)
    {
        var length = array.Length;
        bool swapped;

        for (var i = 0; i < length - 1; i++)
        {
            swapped = false;
            for (var j = 0; j < length - i - 1; j++)
            {
                if (array[j] > array[j + 1])
                {
                    Swap(array, j, j + 1);
                    swapped = true;
                }
            }

            if (!swapped) break;
        }

        return new()
        {
            SortedArray = array,
            SortingAlgorithm = SortingAlgorithm.BubbleSort
        };
    }

    private SortingResultDTO SelectionSort(int[] array)
    {
        throw new NotImplementedException();
    }

    private void Swap(int[] array, int leftIndex, int rightIndex)
    {
        var temp = array[leftIndex];
        array[leftIndex] = array[rightIndex];
        array[rightIndex] = temp;
    }
}
