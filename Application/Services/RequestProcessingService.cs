using Application.Utilities;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.DTO;
using Domain.Models.Validation;
using Domain.Models.Enums;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class RequestProcessingService : IRequestProcessingService
{
    private const string SuccessfullySortedMessage = "Application successfully sorted and saved the input array.";
    private const string SuccessfullyRetrievedMessage = "Application successfully retrieved the latest saved array.";

    private readonly IArrayRepository _arrayRepository;
    private readonly ISortingService _sortingService;
    private readonly ILogger<RequestProcessingService> _logger;

    public RequestProcessingService(
        IArrayRepository arrayRepository,
        ISortingService sortingService,
        ILogger<RequestProcessingService> logger)
    {
        _arrayRepository = arrayRepository;
        _sortingService = sortingService;
        _logger = logger;
    }

    public async Task<SortingOutputDTO> SortAsync(SortingInputDTO sortingInputDTO)
    {
        var numberArray = ArraySerializationUtility.Deserialize(sortingInputDTO.NumberLine);

        var sortingResult = _sortingService.Sort(numberArray, sortingInputDTO.SortingAlgorithm);

        await _arrayRepository.SaveAsync(sortingResult.SortedArray);

        _logger.LogInformation(SuccessfullySortedMessage);

        return MapSortingOutputDTO(sortingResult);
    }

    public async Task<IEnumerable<SortingOutputDTO>> SortAllAlgorithmsAsync(string numberLine)
    {
        var numberArray = ArraySerializationUtility.Deserialize(numberLine);

        var resultList = new List<SortingResultDTO>();
        var algorithms = Enum.GetValues<SortingAlgorithm>();

        var tasks = algorithms.Select(async algorithm =>
        {
            var sortingResult = _sortingService.Sort(numberArray, algorithm);
            await _arrayRepository.SaveAsync(sortingResult.SortedArray);
            resultList.Add(sortingResult);
        });
        await Task.WhenAll(tasks);

        _logger.LogInformation(SuccessfullySortedMessage);

        return resultList.Select(x => MapSortingOutputDTO(x));
    }

    public async Task<string> GetLatestAsync()
    {
        var latestNumberLine = await _arrayRepository.GetLatestAsync();

        if (latestNumberLine is null)
        {
            throw new ArrayNotFoundException();
        }

        NumberLineValidation.Validate(latestNumberLine);

        _logger.LogInformation(SuccessfullyRetrievedMessage);

        return latestNumberLine;
    }

    private SortingOutputDTO MapSortingOutputDTO(SortingResultDTO sortingResult) =>
        new()
        {
            NumberLine = ArraySerializationUtility.Serialize(sortingResult.SortedArray),
            SortingAlgorithm = sortingResult.SortingAlgorithm,
            CalculationTimeInMilliseconds = $"{sortingResult.CalculationTime.TotalMilliseconds:N5} ms"
        };
}
