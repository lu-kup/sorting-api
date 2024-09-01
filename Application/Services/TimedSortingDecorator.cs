using Domain.Interfaces.Services;
using Domain.Models.DTO;
using Domain.Models.Enums;
using System.Diagnostics;

namespace Application.Services;

public class TimedSortingDecorator : ISortingService
{
    private readonly ISortingService _sortingService;
    private readonly Stopwatch _stopwatch = new();

    public TimedSortingDecorator(ISortingService sortingService)
    {
        _sortingService = sortingService;
    }

    public SortingResultDTO Sort(int[] inputArray, SortingAlgorithm sortingAlgorithm)
    {
        _stopwatch.Start();
        var sortingResultDTO = _sortingService.Sort(inputArray, sortingAlgorithm);
        _stopwatch.Stop();

        return sortingResultDTO with
        {
            CalculationTime = _stopwatch.Elapsed
        };
    }
}
