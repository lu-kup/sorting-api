using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Services;
using Domain.Models.DTO;

namespace SortingApi.Controllers;

[Produces("application/json")]
[Route("api/sorting")]
[ApiController]
public class SortingController : ControllerBase
{
    private readonly IDataProcessingService _dataProcessingService;

    public SortingController(IDataProcessingService dataProcessingService)
    {
        _dataProcessingService = dataProcessingService;
    }

    [ProducesResponseType(201)]
    [HttpPost]
    public async Task<ActionResult<SortingOutputDTO>> Sort([FromBody] SortingInputDTO sortingInputDTO)
    {
        var sortingOutputDTO = await _dataProcessingService.SortAsync(sortingInputDTO);

        return StatusCode(201, sortingOutputDTO);
    }

    [ProducesResponseType(200)]
    [HttpGet]
    public async Task<ActionResult> GetLatest()
    {
        var sortingOutputDTO = await _dataProcessingService.GetLatestAsync();

        return Ok(sortingOutputDTO);
    }
}
