using System.Diagnostics;
using Domain.Interfaces.Services;
using Domain.Models.DTO;

namespace Application.Services;

public class TimedSortingDecorator : ISortingService
{
    private readonly ISortingService _sortingService;
    private readonly Stopwatch _stopwatch = new();

    public TimedSortingDecorator(ISortingService sortingService)
    {
        _sortingService = sortingService;
    }

    public SortingOutputDTO Sort(SortingInputDTO sortingInputDTO)
    {
        _stopwatch.Start();
        var sortingOutputDTO = _sortingService.Sort(sortingInputDTO);
        _stopwatch.Stop();

        return sortingOutputDTO with
        {
            calculationTime = _stopwatch.Elapsed
        };
    }
}
