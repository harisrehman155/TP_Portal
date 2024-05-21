using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TP_Portal.Repositories;
using TP_Portal.ViewModel;

namespace TP_Portal.Controllers;

[Authorize(Roles = "Digitizer")]
[ApiController]
[Route("[controller]")]
public class DigitizerController : ControllerBase
{
    private readonly IDigitizerRepository _digitizerRepository;
    public DigitizerController(IDigitizerRepository digitizerRepository)
    {
        _digitizerRepository = digitizerRepository;
    }

    private string GetUserId()
    {
        return User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
    }

    [HttpGet("GetMyDigitizingOrders")]
    public async Task<ActionResult<ApiResponse>> GetMyDigitizingOrders([FromQuery] string? orderId = null)
    {
        ApiResponse _response = await _digitizerRepository.GetMyDigitizingOrdersAsync(GetUserId(), orderId);
        return Ok(_response);
    }

    [HttpPost("UploadDigitizingOrder")]
    public async Task<ActionResult<ApiResponse>> UploadDigitizingOrder([FromForm] MediaViewModel mediaViewModel, [Required][FromQuery] string? orderId)
    {
        ApiResponse _response = await _digitizerRepository.UploadDigitizingOrderAsync(mediaViewModel, GetUserId(), orderId);
        return Ok(_response);
    }

}
