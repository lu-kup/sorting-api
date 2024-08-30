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
    public void Sort_GivenValidInputNumberLine_ReturnsResultWithCalculationTime(int calculationMilliseconds)
    {
        // Arrange
        var simulatedCalculationTime = TimeSpan.FromMilliseconds(calculationMilliseconds);

        var numberLineMock = new int[] { 1 };
        var sortingAlgorithmMock = SortingAlgorithm.BubbleSort;

        var sortingResultMock = new SortingResultDTO()
        {
            SortedArray = new int[] { 1 },
            SortingAlgorithm = sortingAlgorithmMock
        };

        _sortingServiceMock
            .Setup(x => x.Sort(It.IsAny<int[]>(), It.IsAny<SortingAlgorithm>()))
            .Callback(() => Thread.Sleep(simulatedCalculationTime))
            .Returns(sortingResultMock);

        // Act
        var timedSortingResult = _timedSortingDecorator.Sort(numberLineMock, sortingAlgorithmMock);

        // Assert
        Assert.True(timedSortingResult.CalculationTime >= simulatedCalculationTime);
        Assert.Equal(sortingResultMock.SortedArray, timedSortingResult.SortedArray);
        Assert.Equal(sortingResultMock.SortingAlgorithm, timedSortingResult.SortingAlgorithm);
    }
}
