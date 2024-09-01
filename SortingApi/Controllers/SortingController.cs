using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Services;
using Domain.Models.DTO;

namespace SortingApi.Controllers;

[Produces("application/json")]
[Route("api/sorting")]
[ApiController]
public class SortingController : ControllerBase
{
    private readonly IRequestProcessingService _requestProcessingService;

    public SortingController(IRequestProcessingService RequestProcessingService)
    {
        _requestProcessingService = RequestProcessingService;
    }

    [ProducesResponseType(201)]
    [HttpPost]
    public async Task<ActionResult<SortingOutputDTO>> Sort([FromQuery] SortingInputDTO sortingInputDTO)
    {
        var sortingOutputDTO = await _requestProcessingService.SortAsync(sortingInputDTO);

        return StatusCode(201, sortingOutputDTO);
    }

    [ProducesResponseType(201)]
    [HttpPost("all-algorithms")]
    public async Task<ActionResult<IEnumerable<SortingOutputDTO>>> SortAllAlgorithms([FromQuery] string numberLine)
    {
        var sortingOutputList = await _requestProcessingService.SortAllAlgorithmsAsync(numberLine);

        return StatusCode(201, sortingOutputList);
    }

    [ProducesResponseType(200)]
    [HttpGet]
    public async Task<ActionResult<string>> GetLatest()
    {
        var latestNumberLine = await _requestProcessingService.GetLatestAsync();

        return Ok(latestNumberLine);
    }
}
