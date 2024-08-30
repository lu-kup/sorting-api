using Application.Services;
using Domain.Interfaces.Services;
using Domain.Models.DTO;
using Moq;
using Domain.Models.Enums;

namespace UnitTests.Services;

public class TimedSortingDecoratorTests
{
    private readonly ISortingService _timedSortingDecorator;
    private readonly Mock<ISortingService> _sortingServiceMock = new();

    public TimedSortingDecoratorTests()
    {
        _timedSortingDecorator = new TimedSortingDecorator(_sortingServiceMock.Object);
    }

    [Theory]
    [InlineData(100)]
    [InlineData(1000)]
    [InlineData(2000)]
    public async Task SortAsync_GivenValidInputNumberLine_ReturnsResultWithCalculationTime(int calculationMilliseconds)
    {
        // Arrange
        var simulatedCalculationTime = TimeSpan.FromMilliseconds(calculationMilliseconds);

        var numberLineMock = new int[] { 1 };
        var sortingAlgorithmMock = SortingAlgorithm.BubbleSort;

        var sortingOutputMock = new SortingOutputDTO()
        {
            SortedArray = new int[] { 1 },
            SortingAlgorithm = sortingAlgorithmMock
        };

        _sortingServiceMock
            .Setup(x => x.Sort(It.IsAny<int[]>(), It.IsAny<SortingAlgorithm>()))
            .Callback(() => Thread.Sleep(simulatedCalculationTime))
            .Returns(sortingOutputMock);

        // Act
        var timedSortingOutput = _timedSortingDecorator.Sort(numberLineMock, sortingAlgorithmMock);

        // Assert
        Assert.True(timedSortingOutput.CalculationTime > simulatedCalculationTime);
        Assert.Equal(sortingOutputMock.SortedArray, timedSortingOutput.SortedArray);
        Assert.Equal(sortingOutputMock.SortingAlgorithm, timedSortingOutput.SortingAlgorithm);
    }
}
