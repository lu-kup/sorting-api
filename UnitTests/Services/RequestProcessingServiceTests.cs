using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using Application.Services;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.DTO;
using Moq;
using Domain.Models.Enums;

namespace UnitTests.Services;

public class RequestProcessingServiceTests
{
    private readonly IRequestProcessingService _requestProcessingService;
    private readonly Mock<IArrayRepository> _arrayRepositoryMock = new();
    private readonly Mock<ISortingService> _sortingServiceMock = new();
    private readonly Mock<ILogger<RequestProcessingService>> _loggerMock = new();

    public RequestProcessingServiceTests()
    {
        _requestProcessingService = new RequestProcessingService(
            _arrayRepositoryMock.Object,
            _sortingServiceMock.Object,
            _loggerMock.Object);
    }

    [Theory]
    [InlineData("   132 ", new int[] { 132 })]
    [InlineData(" 10 0   3 3 ", new int[] { 10, 0, 3, 3 })]
    [InlineData("1    1 1", new int[] { 1, 1, 1 })]
    [InlineData(" 34534 345  22 34  635 ", new int[] { 34534, 345, 22, 34, 635 })]
    public async Task SortAsync_GivenValidInputNumberLine_ParsesCorrectly(
        string numberLine,
        int[] expectedArray)
    {
        // Arrange
        var sortingInput = new SortingInputDTO()
        {
            NumberLine = numberLine,
            SortingAlgorithm = SortingAlgorithm.BubbleSort
        };

        var sortingResultMock = new SortingResultDTO()
        {
            SortedArray = new int[] { 1 },
            SortingAlgorithm = SortingAlgorithm.BubbleSort
        };

        int[]? parsedArray = null;
        _sortingServiceMock
            .Setup(x => x.Sort(It.IsAny<int[]>(), It.IsAny<SortingAlgorithm>()))
            .Callback<int[], SortingAlgorithm>((outputArray, _) => parsedArray = outputArray)
            .Returns(sortingResultMock);

        // Act
        await _requestProcessingService.SortAsync(sortingInput);

        // Assert
        Assert.Equal(expectedArray, parsedArray);
    }

    [Theory]
    [InlineData("132abc")]
    [InlineData("10-00-3-00")]
    [InlineData("labas")]
    [InlineData("4 5 2 2,34 5")]
    [InlineData(" ")]
    public async Task SortAsync_GivenInvalidInputNumberLine_Throws(string numberLine)
    {
        // Arrange
        var sortingInput = new SortingInputDTO()
        {
            NumberLine = numberLine,
            SortingAlgorithm = SortingAlgorithm.BubbleSort
        };

        // Act and assert
        await Assert.ThrowsAsync<ValidationException>(
            async () => await _requestProcessingService.SortAsync(sortingInput));
    }

    [Fact]
    public async Task SortAllAlgorithmsAsync_GivenValidInputNumberLine_SortsMultipleTimes()
    {
        // Arrange
        var numberLineMock = "34534 345  22 34  635";

        var sortingResultMock = new SortingResultDTO()
        {
            SortedArray = new int[] { 1 },
            SortingAlgorithm = SortingAlgorithm.BubbleSort
        };

        _sortingServiceMock
            .Setup(x => x.Sort(It.IsAny<int[]>(), It.IsAny<SortingAlgorithm>()))
            .Returns(sortingResultMock);

        var expectedCount = Enum.GetValues<SortingAlgorithm>().Count();

        // Act
        var resultList = await _requestProcessingService.SortAllAlgorithmsAsync(numberLineMock);

        // Assert
        Assert.Equal(expectedCount, resultList.Count());
        _sortingServiceMock.Verify(x => x.Sort(It.IsAny<int[]>(), SortingAlgorithm.BubbleSort), Times.Once);
        _sortingServiceMock.Verify(x => x.Sort(It.IsAny<int[]>(), SortingAlgorithm.InsertionSort), Times.Once);
        _sortingServiceMock.Verify(x => x.Sort(It.IsAny<int[]>(), SortingAlgorithm.SelectionSort), Times.Once);
        _sortingServiceMock.Verify(x => x.Sort(It.IsAny<int[]>(), SortingAlgorithm.QuickSort), Times.Once);
        _sortingServiceMock.Verify(x => x.Sort(It.IsAny<int[]>(), SortingAlgorithm.MergeSort), Times.Once);
    }

    [Fact]
    public async Task GetLatestAsync_WithNoArraysFound_Throws()
    {
        // Arrange
        _arrayRepositoryMock.Setup(
            x => x.GetLatestAsync()).ReturnsAsync((string?)null);

        // Act and assert
        await Assert.ThrowsAsync<ArrayNotFoundException>(
            async () => await _requestProcessingService.GetLatestAsync());
    }
}
