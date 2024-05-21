using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TP_Portal.Repositories;
using TP_Portal.ViewModel;

namespace TP_Portal.Controllers;

[Authorize(Roles = "VectorArtist")]
[ApiController]
[Route("[controller]")]
public class VectorArtistController : ControllerBase
{
    private readonly IVectorArtistRepository _VectorArtistRepository;
    public VectorArtistController(IVectorArtistRepository VectorArtistRepository)
    {
        _VectorArtistRepository = VectorArtistRepository;
    }

    private string GetUserId()
    {
        return User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
    }

    [HttpGet("GetMyVectorOrders")]
    public async Task<ActionResult<ApiResponse>> GetMyVectorOrders([FromQuery] string? orderId = null)
    {
        ApiResponse _response = await _VectorArtistRepository.GetMyVectorOrdersAsync(GetUserId(), orderId);
        return Ok(_response);
    }

    [HttpPost("UploadVectorOrder")]
    public async Task<ActionResult<ApiResponse>> UploadVectorOrder([FromForm] MediaViewModel mediaViewModel, [FromQuery] string? orderId = null)
    {
        ApiResponse _response = await _VectorArtistRepository.UploadVectorOrderAsync(mediaViewModel, GetUserId(), orderId);
        return Ok(_response);
    }

}
