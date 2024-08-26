using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.Extensions.Logging;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.DTO;
using Domain.Models.Validation;

namespace Application.Services;

public class DataProcessingService : IDataProcessingService
{
    private const string SuccessfullySortedMessage = "Application successfully sorted and saved the input array.";
    private const string SuccessfullyRetrievedMessage = "Application successfully retrieved the latest saved array.";

    private readonly IArrayRepository _arrayRepository;
    private readonly ISortingService _sortingService;
    private readonly ILogger<DataProcessingService> _logger;

    public DataProcessingService(
        IArrayRepository arrayRepository,
        ISortingService sortingService,
        ILogger<DataProcessingService> logger)
    {
        _arrayRepository = arrayRepository;
        _sortingService = sortingService;
        _logger = logger;
    }

    public async Task<SortingOutputDTO> SortAsync(SortingInputDTO sortingInputDTO)
    {
        var numberArray = ParseNumberLine(sortingInputDTO.NumberLine);

        var sortingOutputDTO = _sortingService.Sort(numberArray, sortingInputDTO.SortingAlgorithm);

        await _arrayRepository.SaveArrayAsync(sortingOutputDTO.SortedArray);

        _logger.LogInformation(SuccessfullySortedMessage);

        return sortingOutputDTO;
    }

    public async Task<int[]> GetLatestAsync()
    {
        var latestArray = await _arrayRepository.GetLatestArrayAsync();

        if (latestArray is null)
        {
            throw new ArrayNotFoundException();
        }

        _logger.LogInformation(SuccessfullyRetrievedMessage);

        return latestArray;
    }

    private int[] ParseNumberLine(string numberLine)
    {
        NumberLineValidation.Validate(numberLine);

        var stringArray = numberLine.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var intArray = Array.ConvertAll(stringArray, int.Parse);

        return intArray;
    }
}
