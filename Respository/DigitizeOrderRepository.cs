
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TP_Portal.Context;
using TP_Portal.Helper;
using TP_Portal.Model.MyApplicationUser;
using TP_Portal.ViewModel;


namespace TP_Portal.Repositories;

public interface IDigitizeOrderRepository
{
    Task<ApiResponse> GetAllDigitizeOrderAsync(string userId, string OrderId);
    Task<ApiResponse> CreateDigitizeOrderAsync(CreateDigitizeOrderVM createOrderAsync, string userId);
    Task<ApiResponse> UpdateDigitizeOrderAsync(UpdateDigitizeOrderVM updateDigitizeOrderVM, string userId);
    Task<ApiResponse> DeleteDigitizeOrderAsync(string orderId, string userId);
}
public class DigitizeOrderRepository : IDigitizeOrderRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;
    private readonly ImageHelper _imgHelper;
    private readonly MyHelperFunc _myHelperFunc;

    public DigitizeOrderRepository(UserManager<ApplicationUser> userManager, ApplicationDbContext context, ImageHelper imgHelper, MyHelperFunc myHelperFunc)
    {
        _userManager = userManager;
        _context = context;
        _imgHelper = imgHelper;
        _myHelperFunc = myHelperFunc;
    }

    private ApiResponse UnauthorizedResponse()
    {
        return HelperFunc.MyApiResponse(false, StatusCodes.Status401Unauthorized, "Unauthorized access!", null);
    }

    public async Task<ApiResponse> GetAllDigitizeOrderAsync(string userId, string orderId)
    {
        try
        {
            // Validate user ID
            if (string.IsNullOrEmpty(userId))
                return UnauthorizedResponse();

            // Parse orderId to Guid
            Guid parsedOrderId = string.IsNullOrEmpty(orderId) ? Guid.Empty : new Guid(orderId);

            // Get the OrderTypeId for "Digitize" asynchronously
            var digitizeOrderTypeId = await _myHelperFunc.GetOrderTypeIdAsync("Digitize");

            // Retrieve digitized orders with the specified UserId, OrderId, and OrderTypeId
            var digitizeRecords = await _context.Orders
                .Where(order => order.UserId == userId
                    && (parsedOrderId == Guid.Empty || order.Id == parsedOrderId)
                    && order.OrderTypeId == digitizeOrderTypeId)
                .Select(order => new GetAllDigitizeOrdersVM
                {
                    Id = order.Id,
                    PoNo = order.PoNo,
                    Date = order.Date,
                    Name = order.Name,
                    Description = order.Description,
                    Fabric = order.Fabric,
                    NoOfColor = order.NoOfColor,
                    Height = order.Height,
                    Width = order.Width,
                    Placement = order.Placement,
                    IsUrgent = order.IsUrgent,
                    UpdatedDate = order.UpdatedDate,
                    OrderMedia = order.OrderMedia.Where(x=>x.IsUploadedByCustomer)
                        .Select(media => new MediaViewModel
                        {
                            ImageUrl = media.ImageUrl
                        })
                        .ToList()
                })
                .OrderBy(x => x.PoNo)
                .ToListAsync();

            if (digitizeRecords.Any())
                return HelperFunc.MyApiResponse(true, StatusCodes.Status200OK, "Records fetched successfully!", digitizeRecords);
            else
                return HelperFunc.MyApiResponse(false, StatusCodes.Status404NotFound, "No records found!", null);
        }
        catch (DbUpdateException ex)
        {
            // Log the database update exception
            Console.WriteLine($"Database update error: {ex.Message}");
            return HelperFunc.MyApiResponse(false, StatusCodes.Status500InternalServerError, $"An error occurred while updating the database.", null);
        }
        catch (Exception ex)
        {
            // Log other exceptions
            Console.WriteLine($"An error occurred: {ex.Message}");
            return HelperFunc.MyApiResponse(false, StatusCodes.Status500InternalServerError, $"An error occurred while fetching records. Inner Exception: {ex.Message}", null);
        }
    }


    public async Task<ApiResponse> CreateDigitizeOrderAsync(CreateDigitizeOrderVM createDigitizeOrderVM, string userId)
    {
        try
        {
            // Validate user ID
            if (string.IsNullOrEmpty(userId))
                return UnauthorizedResponse();

            var request = createDigitizeOrderVM;
            if (request == null)
                return HelperFunc.MyApiResponse(false, StatusCodes.Status400BadRequest, "Invalid request!", null);

            // Fetch the maximum PoNo from the database
            var lastPoNo = await _context.Orders.OrderBy(x => x.PoNo).LastOrDefaultAsync();
            var poNo = lastPoNo == null ? 1 : lastPoNo.PoNo + 1;

            // Upload and save order media and returns list of save images url
            var orderMediaList = await UploadAndSaveImagesAsync(request.OrderMedia?.Images, poNo.ToString());
            var digitizeOrderTypeId = await _myHelperFunc.GetOrderTypeIdAsync("Digitize");

            // Create a new order
            var newOrder = new Order
            {
                Id = Guid.NewGuid(),
                PoNo = poNo,
                Date = DateTime.Now,
                Name = request.Name,
                Description = request.Description,
                Height = request.Height,
                Width = request.Width,
                NoOfColor = request.NoOfColor,
                Fabric = request.Fabric,
                IsUrgent = request.IsUrgent,
                Placement = request.Placement,
                UserId = userId,
                OrderTypeId = digitizeOrderTypeId,
                OrderMedia = orderMediaList
            };

            // Add the new order to the database
            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            return HelperFunc.MyApiResponse(true, StatusCodes.Status201Created, "Order Created Successfully!", null);
        }
        catch (DbUpdateException ex)
        {
            // Log the database update exception
            Console.WriteLine($"Database update error: {ex.Message}");
            return HelperFunc.MyApiResponse(false, StatusCodes.Status500InternalServerError, $"An error occurred while updating the database.", null);
        }
        catch (Exception ex)
        {
            // Log other exceptions
            Console.WriteLine($"An error occurred: {ex.Message}");
            return HelperFunc.MyApiResponse(false, StatusCodes.Status500InternalServerError, $"Exception Occurred While Creating Order. Inner Exception: {ex.Message}", null);
        }
    }


    public async Task<ApiResponse> UpdateDigitizeOrderAsync(UpdateDigitizeOrderVM updateDigitizeOrderVM, string userId)
    {
        try
        {
            if (string.IsNullOrEmpty(userId))
                return UnauthorizedResponse();

            var request = updateDigitizeOrderVM;
            if (request == null)
                return HelperFunc.MyApiResponse(false, StatusCodes.Status400BadRequest, "Invalid request!", null);

            var digitizeOrderTypeId = await _myHelperFunc.GetOrderTypeIdAsync("Digitize");
            var orderRecord = await _context.Orders
                .Include(o => o.OrderMedia)
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.OrderTypeId == digitizeOrderTypeId);

            if (orderRecord == null)
                return HelperFunc.MyApiResponse(false, StatusCodes.Status404NotFound, "Order not found!", null);

            var orderMediaList = await UploadAndSaveImagesAsync(request.OrderMedia?.Images, orderRecord.PoNo.ToString());

            if (!orderMediaList.IsNullOrEmpty())
            {
                orderRecord.OrderMedia.Clear();
                orderRecord.OrderMedia.AddRange(orderMediaList);
            }

            orderRecord.Name = request.Name;
            orderRecord.Description = request.Description;
            orderRecord.Height = request.Height;
            orderRecord.Width = request.Width;
            orderRecord.NoOfColor = request.NoOfColor;
            orderRecord.Fabric = request.Fabric;
            orderRecord.IsUrgent = request.IsUrgent;
            orderRecord.Placement = request.Placement;
            orderRecord.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return HelperFunc.MyApiResponse(true, StatusCodes.Status200OK, "Order updated successfully!", null);
        }
        catch (Exception ex)
        {
            return HelperFunc.MyApiResponse(false, StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}", null);
        }
    }

    public async Task<ApiResponse> DeleteDigitizeOrderAsync(string orderId, string userId)
    {
        try
        {
            if (string.IsNullOrEmpty(userId))
                return UnauthorizedResponse();

            Guid parsedOrderId = string.IsNullOrEmpty(orderId) ? Guid.Empty : new Guid(orderId);
            var digitizeOrderTypeId = await _myHelperFunc.GetOrderTypeIdAsync("Digitize");

            var digitizeRecord = await _context.Orders
                .FirstOrDefaultAsync(order => order.UserId == userId 
                && (parsedOrderId == Guid.Empty || order.Id == parsedOrderId)
                && order.OrderTypeId == digitizeOrderTypeId);

            if (digitizeRecord != null)
            {
                _context.Orders.Remove(digitizeRecord);
                await _context.SaveChangesAsync();
                return HelperFunc.MyApiResponse(true, StatusCodes.Status200OK, "Record deleted successfully!", null);
            }
            else
            {
                return HelperFunc.MyApiResponse(false, StatusCodes.Status404NotFound, "No record found!", null);
            }
        }
        catch (DbUpdateException ex)
        {
            return HelperFunc.MyApiResponse(false, StatusCodes.Status500InternalServerError, $"An error occurred while deleting the record. Database error: {ex.Message}", null);
        }
        catch (Exception ex)
        {
            return HelperFunc.MyApiResponse(false, StatusCodes.Status500InternalServerError, $"An error occurred while deleting the record: {ex.Message}", null);
        }
    }

    private async Task<List<OrderMedia>> UploadAndSaveImagesAsync(IFormFileCollection? images, string orderNo)
    {
        if (images == null || images.Count == 0)
            return new List<OrderMedia>();

        var (savedImagesPath, _) = await _imgHelper.UploadMultiImage(images, orderNo);

        return savedImagesPath.Select(imagePath => new OrderMedia
        {
            // Id = Guid.NewGuid(),
            ImageUrl = imagePath,
            IsUploadedByCustomer = true
        }).ToList();
    }

}
