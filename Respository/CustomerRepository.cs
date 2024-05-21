using System.IO.Compression;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TP_Portal.Context;
using TP_Portal.Helper;
using TP_Portal.Model.MyApplicationUser;
using TP_Portal.ViewModel;

namespace TP_Portal.Repositories;

public interface ICustomerRepository
{
    Task<(string poNo, string path)> DownloadOrderAsync(string userId, string OrderId);
}
public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ImageHelper _imgHelper;
    private readonly IWebHostEnvironment _environment;

    public CustomerRepository(ApplicationDbContext context, ImageHelper imgHelper, IWebHostEnvironment environment)
    {
        _context = context;
        _imgHelper = imgHelper;
        _environment = environment;
    }

    private ApiResponse UnauthorizedResponse()
    {
        return HelperFunc.MyApiResponse(false, StatusCodes.Status401Unauthorized, "Unauthorized access!", null);
    }

    public async Task<(string poNo, string path)> DownloadOrderAsync(string userId, string orderId)
    {
        try
        {
            if (string.IsNullOrEmpty(userId))
                return ("0", "");

            if (string.IsNullOrEmpty(orderId) || !Guid.TryParse(orderId, out Guid parsedOrderId))
                return ("0", "");

            var record = await _context.Orders.Where(x => x.Id == parsedOrderId).FirstOrDefaultAsync();

            if (record == null)
                return ("0", "");

            var orderNo = record?.PoNo.ToString();

            if (string.IsNullOrEmpty(orderNo))
                return ("0", "");

            // Path to the Upload folder inside wwwroot
            string uploadFolder = Path.Combine(_environment.WebRootPath, "Download");

            // Path to the order-specific folder inside Upload folder
            string orderFolderPath = Path.Combine(uploadFolder, orderNo);

            return (orderNo, orderFolderPath);
        }
        catch (IOException ioEx)
        {
            Console.WriteLine($"An IO error occurred: {ioEx.Message}");
            return ("0", "");
        }
        catch (Exception ex)
        {
            // Log the exception
            Console.WriteLine($"An error occurred: {ex.Message}");
            return ("0", "");
        }
    }




}


