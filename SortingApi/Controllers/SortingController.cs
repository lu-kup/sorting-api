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
    public async Task<ActionResult<SortingOutputDTO>> Sort([FromBody] SortingInputDTO sortingInputDTO)
    {
        var sortingOutputDTO = await _requestProcessingService.SortAsync(sortingInputDTO);

        return StatusCode(201, sortingOutputDTO);
    }

    [ProducesResponseType(200)]
    [HttpGet]
    public async Task<ActionResult> GetLatest()
    {
        var sortingOutputDTO = await _requestProcessingService.GetLatestAsync();

        return Ok(sortingOutputDTO);
    }
}
