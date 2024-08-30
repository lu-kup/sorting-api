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

public class DataProcessingServiceTests
{
    private const int MockBookId = 123;

    private readonly Mock<IArrayRepository> _arrayRepositoryMock = new();
    private readonly Mock<ISortingService> _sortingServiceMock = new();
    private readonly Mock<ILogger<DataProcessingService>> _loggerMock = new();
    private readonly IDataProcessingService _dataProcessingService;

    public DataProcessingServiceTests()
    {
        _dataProcessingService = new DataProcessingService(
            _arrayRepositoryMock.Object,
            _sortingServiceMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task GetLatestAsync_WithNoArraysFound_Throws()
    {
        // Arrange
        _arrayRepositoryMock.Setup(
            x => x.GetLatestArrayAsync()).ReturnsAsync((string?) null);

        // Act and assert
        await Assert.ThrowsAsync<ArrayNotFoundException>(
            async () => await _dataProcessingService.GetLatestAsync());
    }

    [Theory]
    [InlineData("132abc")]
    [InlineData("10-00-3-00")]
    [InlineData("labas")]
    [InlineData("4 5 2 2,34 5")]
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
            async () => await _dataProcessingService.SortAsync(sortingInput));
    }

    [Theory]
    [InlineData("132", new int[] { 132 })]
    [InlineData("10 0 3 3", new int[] { 10, 0, 3, 3 })]
    [InlineData("1 1 1", new int[] { 1, 1, 1 })]
    [InlineData("34534 345  22 34  635", new int[] { 34534, 345, 33, 34, 635 })]
    public async Task SortAsync_GivenValidInputNumberLine_ParsesCorrectly(string numberLine, int[] expectedArray)
    {
        // Arrange
        var sortingInput = new SortingInputDTO()
        {
            NumberLine = numberLine,
            SortingAlgorithm = SortingAlgorithm.BubbleSort
        };

        int[] parsedArray = null;
        _sortingServiceMock
            .Setup(x => x.Sort(It.IsAny<int[]>(), It.IsAny<SortingAlgorithm>()))
            .Callback<int[], SortingAlgorithm>((outputArray, _) => parsedArray = outputArray);

        // Act
        await _dataProcessingService.SortAsync(sortingInput);

        // Assert
        Assert.Equal(expectedArray, parsedArray);
    }
}
