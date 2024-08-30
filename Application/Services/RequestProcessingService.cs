using Microsoft.Extensions.Logging;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.DTO;
using Application.Utilities;

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

        var sortingOutputDTO = _sortingService.Sort(numberArray, sortingInputDTO.SortingAlgorithm);

        await _arrayRepository.SaveAsync(sortingOutputDTO.SortedArray);

        _logger.LogInformation(SuccessfullySortedMessage);

        return sortingOutputDTO;
    }

    public async Task<string> GetLatestAsync()
    {
        var latestArray = await _arrayRepository.GetLatestAsync();

        if (latestArray is null)
        {
            throw new ArrayNotFoundException();
        }

        _logger.LogInformation(SuccessfullyRetrievedMessage);

        return latestArray;
    }
}
