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
            SortingAlgorithm.InsertionSort => InsertionSort(inputArray),
            SortingAlgorithm.QuickSort => QuickSort(inputArray),
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
        var length = array.Length;

        for (var i = 0; i < length - 1; i++)
        {
            var indexSmallest = i;
    
            for (var j = i + 1; j < length; j++)
            {
                if (array[j] < array[indexSmallest])
                {
                    indexSmallest = j;
                }
            }

            if (i != indexSmallest)
            {
                Swap(array, i, indexSmallest);
            }
        }

        return new()
        {
            SortedArray = array,
            SortingAlgorithm = SortingAlgorithm.SelectionSort
        };
    }

    private SortingResultDTO InsertionSort(int[] array)
    {
        var length = array.Length;

        for (var i = 1; i < length; ++i)
        {
            var currentValue = array[i];
            var j = i - 1;

            while (j >= 0 && array[j] > currentValue)
            {
                array[j + 1] = array[j];
                j--;
            }
            array[j + 1] = currentValue;
        }

        return new()
        {
            SortedArray = array,
            SortingAlgorithm = SortingAlgorithm.InsertionSort
        };
    }

    private SortingResultDTO QuickSort(int[] array)
    {
        var length = array.Length;

        RunQuickSort(array, 0, length - 1);

        return new()
        {
            SortedArray = array,
            SortingAlgorithm = SortingAlgorithm.QuickSort
        };
    }

    private void RunQuickSort(int[] array, int lowIndex, int highIndex)
    {
        if (lowIndex >= highIndex) return;

        var partitionIndex = Partition(array, lowIndex, highIndex);
        RunQuickSort(array, lowIndex, partitionIndex - 1);
        RunQuickSort(array, partitionIndex + 1, highIndex);
    }

    private int Partition(int[] array, int lowIndex, int highIndex)
    {
        var pivotValue = array[highIndex];
        var i = lowIndex - 1;

        for (var j = lowIndex; j < highIndex; j++)
        {
            if (array[j] < pivotValue)
            {
                i++;
                Swap(array, i, j);
            }
        }
        i++;
        Swap(array, i, highIndex);

        return i;
    }

    private void Swap(int[] array, int leftIndex, int rightIndex)
    {
        var temp = array[leftIndex];
        array[leftIndex] = array[rightIndex];
        array[rightIndex] = temp;
    }
}
