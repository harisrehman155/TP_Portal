using Microsoft.AspNetCore.Mvc;
using TP_Portal.LOVs;

namespace TP_Portal.Controllers;

// [Authorize(Roles = "Admin")]
[ApiController]
[Route("[controller]")]
public class LOVsController : ControllerBase
{
    private readonly ILOVsRepository _lovsRepository;
    public LOVsController(ILOVsRepository lovsRepository)
    {
        _lovsRepository = lovsRepository;
    }

    [HttpGet("GetAllDigitizers")]
    public async Task<ActionResult<ApiResponse>> GetAllDigitizers()
    {
        ApiResponse _response = await _lovsRepository.GetAllDigitizersAsync();
        return Ok(_response);
    }


    [HttpGet("GetAllVectorArtists")]
    public async Task<ActionResult<ApiResponse>> GetAllVectorArtists()
    {
        ApiResponse _response = await _lovsRepository.GetAllVectorArtistsAsync();
        return Ok(_response);
    }
}
