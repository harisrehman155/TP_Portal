using System.IO.Compression;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TP_Portal.Repositories;
using TP_Portal.ViewModel;

namespace TP_Portal.Controllers;

[Authorize(Roles = "Admin,User")]
[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IDigitizeOrderRepository _digitizeOrderRepository;
    private readonly IVectorOrderRepository _vectorOrderRepository;
    private readonly ICustomerRepository _customerRepository;

    public CustomerController(IDigitizeOrderRepository digitizeOrderRepository, IVectorOrderRepository vectorOrderRepository, ICustomerRepository customerRepository)
    {
        _digitizeOrderRepository = digitizeOrderRepository;
        _vectorOrderRepository = vectorOrderRepository;
        _customerRepository = customerRepository;
    }

    private string GetUserId()
    {
        return User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
    }

    #region Digitize

    [HttpGet("GetAllDigitize")]
    public async Task<IActionResult> GetAllDigitize([FromQuery] string? orderId = null)
    {
        var response = await _digitizeOrderRepository.GetAllDigitizeOrderAsync(GetUserId(), orderId);
        return Ok(response);
    }

    [HttpPost("CreateDigitize")]
    public async Task<IActionResult> CreateDigitize([FromForm] CreateDigitizeOrderVM createDigitizeOrderVM)
    {
        var response = await _digitizeOrderRepository.CreateDigitizeOrderAsync(createDigitizeOrderVM, GetUserId());
        return Ok(response);
    }

    [HttpPut("UpdateDigitize")]
    public async Task<IActionResult> UpdateDigitize([FromForm] UpdateDigitizeOrderVM updateDigitizeOrderVM)
    {
        var response = await _digitizeOrderRepository.UpdateDigitizeOrderAsync(updateDigitizeOrderVM, GetUserId());
        return Ok(response);
    }

    [HttpDelete("DeleteDigitize/{orderId}")]
    public async Task<IActionResult> DeleteDigitize(string orderId)
    {
        var response = await _digitizeOrderRepository.DeleteDigitizeOrderAsync(orderId, GetUserId());
        return Ok(response);
    }

    #endregion Digitize
    // ---------------------------------------------------------------------------
    #region Vector
    [HttpGet("GetAllVector")]
    public async Task<IActionResult> GetAllVector([FromQuery] string? orderId = null)
    {
        var response = await _vectorOrderRepository.GetAllVectorOrderAsync(GetUserId(), orderId);
        return Ok(response);
    }

    [HttpPost("CreateVector")]
    public async Task<IActionResult> CreateVector([FromForm] CreateVectorOrderVM createVectorOrderVM)
    {
        var response = await _vectorOrderRepository.CreateVectorOrderAsync(createVectorOrderVM, GetUserId());
        return Ok(response);
    }

    [HttpPut("UpdateVector")]
    public async Task<IActionResult> UpdateVector([FromForm] UpdateVectorOrderVM updateVectorOrderVM)
    {
        var response = await _vectorOrderRepository.UpdateVectorOrderAsync(updateVectorOrderVM, GetUserId());
        return Ok(response);
    }

    [HttpDelete("DeleteVector/{orderId}")]
    public async Task<IActionResult> DeleteVector(string orderId)
    {
        var response = await _vectorOrderRepository.DeleteVectorOrderAsync(orderId, GetUserId());
        return Ok(response);
    }

    #endregion

    //                  -----------------=============*************=============-----------------

    #region  Download Order
    
    [HttpGet("DownloadOrder")]
    public async Task<IActionResult> DownloadOrder([FromQuery] string orderId)
    {
        var (orderNo, orderFolderPath) = await _customerRepository.DownloadOrderAsync(GetUserId(), orderId);
        if (Directory.Exists(orderFolderPath))
        {
            // Create a unique temporary file to store the zip archive
            string zipFilePath = Path.Combine(Path.GetTempPath(), $"{orderNo}.zip");

            // Create a new zip archive for the order-specific folder
            ZipFile.CreateFromDirectory(orderFolderPath, zipFilePath, CompressionLevel.Fastest, false);

            // Read the zip file into a MemoryStream
            MemoryStream memoryStream = new MemoryStream();
            using (FileStream zipStream = new FileStream(zipFilePath, FileMode.Open))
            {
                await zipStream.CopyToAsync(memoryStream);
            }

            // Clean up the temporary zip file
            System.IO.File.Delete(zipFilePath);

            // Reset the MemoryStream position to the beginning
            memoryStream.Position = 0;

            // Return the zip file as a downloadable file with a .zip extension
            return File(memoryStream, "application/zip", $"{orderNo}.zip");
        }
        else
        {
            return NotFound();
        }
    }

    #endregion
}
