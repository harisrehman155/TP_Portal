using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TP_Portal.Repositories;
using TP_Portal.ViewModel;

namespace TP_Portal.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("[controller]")]
public class PricingController : ControllerBase
{
    private readonly IPricingRepository _PricingRepository;
    public PricingController(IPricingRepository PricingRepository)
    {
        _PricingRepository = PricingRepository;
    }

    [HttpGet("GetAllPricing")]
    public async Task<ActionResult<ApiResponse>> GetAllPricing([FromQuery] string customerId = null)
    {
        ApiResponse _response = await _PricingRepository.GetAllPricingAsync(customerId);
        return Ok(_response);
    }

    [HttpPost("CreatePricing")]
    public async Task<IActionResult> CreateDigitize([FromBody] CreatePricingVM createPricingVM)
    {
        var response = await _PricingRepository.CreatePricingAsync(createPricingVM);
        return Ok(response);
    }

    [HttpPut("UpdatePricing")]
    public async Task<IActionResult> UpdateDigitize([FromBody] UpdatePricingVM updatePricingVM)
    {
        var response = await _PricingRepository.UpdatePricingAsync(updatePricingVM);
        return Ok(response);
    }

    [HttpDelete("DeletePricing/{Id}")]
    public async Task<IActionResult> DeleteDigitize(string Id)
    {
        var response = await _PricingRepository.DeletePricingAsync(Id);
        return Ok(response);
    }

}
