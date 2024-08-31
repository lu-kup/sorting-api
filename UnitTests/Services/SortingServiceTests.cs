using Application.Services;
using Domain.Interfaces.Services;
using Domain.Models.Enums;

namespace UnitTests.Services;

public class SortingServiceTests
{
    private readonly ISortingService _sortingService;

    public SortingServiceTests()
    {
        _sortingService = new SortingService();
    }

    [Theory]
    [InlineData(SortingAlgorithm.BubbleSort)]
    [InlineData(SortingAlgorithm.SelectionSort)]
    [InlineData(SortingAlgorithm.InsertionSort)]
    [InlineData(SortingAlgorithm.QuickSort)]
    public void Sort_GivenValidSortingAlgorithm_SortsCorrectly(SortingAlgorithm sortingAlgorithm)
    {
        // Arrange
        var inputArray = new int[] { 10, 2, 30, 1, 3, 3, 2 };
        var expectedSortedArray = new int[] { 1, 2, 2, 3, 3, 10, 30 };

        // Act
        var sortingResult = _sortingService.Sort(inputArray, sortingAlgorithm);

        // Assert
        Assert.Equal(expectedSortedArray, sortingResult.SortedArray);
        Assert.Equal(sortingAlgorithm, sortingResult.SortingAlgorithm);
    }

    [Theory]
    [InlineData(new int[] { 132 }, new int[] { 132 })]
    [InlineData(new int[] { 10, 0, 3, 3 }, new int[] { 0, 3, 3, 10 })]
    [InlineData(new int[] { 1, 1, 1 }, new int[] { 1, 1, 1 })]
    [InlineData(new int[] { 34534, 345, 22, 34, 635 }, new int[] { 22, 34, 345, 635, 34534 })]
    public void BubbleSort_GivenVariousInputArrays_SortsCorrectly(
        int[] inputArray,
        int[] expectedSortedArray)
    {
        // Arrange
        var sortingAlgorithm = SortingAlgorithm.BubbleSort;

        // Act
        var sortingResult = _sortingService.Sort(inputArray, sortingAlgorithm);

        // Assert
        Assert.Equal(expectedSortedArray, sortingResult.SortedArray);
        Assert.Equal(sortingAlgorithm, sortingResult.SortingAlgorithm);
    }

    [Theory]
    [InlineData(new int[] { 132 }, new int[] { 132 })]
    [InlineData(new int[] { 10, 0, 3, 3 }, new int[] { 0, 3, 3, 10 })]
    [InlineData(new int[] { 1, 1, 1 }, new int[] { 1, 1, 1 })]
    [InlineData(new int[] { 34534, 345, 22, 34, 635 }, new int[] { 22, 34, 345, 635, 34534 })]
    public void QuickSort_GivenVariousInputArrays_SortsCorrectly(
        int[] inputArray,
        int[] expectedSortedArray)
    {
        // Arrange
        var sortingAlgorithm = SortingAlgorithm.QuickSort;

        // Act
        var sortingResult = _sortingService.Sort(inputArray, sortingAlgorithm);

        // Assert
        Assert.Equal(expectedSortedArray, sortingResult.SortedArray);
        Assert.Equal(sortingAlgorithm, sortingResult.SortingAlgorithm);
    }
}
