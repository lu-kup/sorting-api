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
    public async Task Sort_GivenValidSortingAlgorithm_SortsArrayCorrectly(SortingAlgorithm sortingAlgorithm)
    {
        // Arrange
        var inputArray = new int[] { 10, 2, 30, 1, 3, 3, 2 };
        var expectedSortedArray = new int[] { 1, 2, 2, 3, 3, 10, 30 };

        // Act
        var sortingOutput = _sortingService.Sort(inputArray, sortingAlgorithm);

        // Assert
        Assert.Equal(expectedSortedArray, sortingOutput.SortedArray);
        Assert.Equal(sortingAlgorithm, sortingOutput.SortingAlgorithm);
    }
}
