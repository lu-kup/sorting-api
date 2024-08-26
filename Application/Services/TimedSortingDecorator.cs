using System.Diagnostics;
using Domain.Interfaces.Services;
using Domain.Models.DTO;
using Domain.Models.Enums;

namespace Application.Services;

public class TimedSortingDecorator : ISortingService
{
    private readonly ISortingService _sortingService;
    private readonly Stopwatch _stopwatch = new();

    public TimedSortingDecorator(ISortingService sortingService)
    {
        _sortingService = sortingService;
    }

    public SortingOutputDTO Sort(int[] inputArray, SortingAlgorithm sortingAlgorithm)
    {
        _stopwatch.Start();
        var sortingOutputDTO = _sortingService.Sort(inputArray, sortingAlgorithm);
        _stopwatch.Stop();

        return sortingOutputDTO with
        {
            CalculationTime = _stopwatch.Elapsed
        };
    }
}
