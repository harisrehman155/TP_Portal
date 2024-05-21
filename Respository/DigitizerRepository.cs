using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TP_Portal.Context;
using TP_Portal.Helper;
using TP_Portal.Model.MyApplicationUser;
using TP_Portal.ViewModel;

namespace TP_Portal.Repositories;

public interface IDigitizerRepository
{
    Task<ApiResponse> GetMyDigitizingOrdersAsync(string userId, string OrderId);
    Task<ApiResponse> UploadDigitizingOrderAsync(MediaViewModel mediaViewModel, string userId, string OrderId);

}
public class DigitizerRepository : IDigitizerRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _context;
    private readonly ImageHelper _imgHelper;

    public DigitizerRepository(
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext context,
        RoleManager<IdentityRole> roleManager,
        ImageHelper imgHelper)
    {
        _userManager = userManager;
        _context = context;
        _roleManager = roleManager;
        _imgHelper = imgHelper;
    }

    private ApiResponse UnauthorizedResponse()
    {
        return HelperFunc.MyApiResponse(false, StatusCodes.Status401Unauthorized, "Unauthorized access!", null);
    }

    public async Task<ApiResponse> GetMyDigitizingOrdersAsync(string userId, string OrderId)
    {
        try
        {
            if (string.IsNullOrEmpty(userId))
                return UnauthorizedResponse();

            Guid parsedOrderId = string.IsNullOrEmpty(OrderId) ? Guid.Empty : new Guid(OrderId);

            var result = await (from assignOrder in _context.AssignOrders
                                join order in _context.Orders on assignOrder.OrderId equals order.Id
                                where assignOrder.EmployeeId == userId && !assignOrder.IsCompleted
                                select new GetAllDigitizeOrdersVM
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
                                    OrderMedia = order.OrderMedia!.Select(m => new MediaViewModel
                                    {
                                        ImageUrl = m.ImageUrl
                                    }).ToList()

                                }).ToListAsync();

            if (result.Any())
                return HelperFunc.MyApiResponse(true, StatusCodes.Status200OK, "Records fetched successfully!", result);
            else
                return HelperFunc.MyApiResponse(false, StatusCodes.Status404NotFound, "No records found!", null);
        }
        catch (Exception ex)
        {
            // Log the exception
            Console.WriteLine($"An error occurred: {ex.Message}");
            return HelperFunc.MyApiResponse(false, StatusCodes.Status500InternalServerError, $"An error occurred while fetching records. Inner Exception: {ex.Message}", null);
        }
    }

    public async Task<ApiResponse> UploadDigitizingOrderAsync(MediaViewModel mediaViewModel, string userId, string OrderId)
    {
        try
        {
            if (string.IsNullOrEmpty(userId))
                return UnauthorizedResponse();

            Guid parsedOrderId = string.IsNullOrEmpty(OrderId) ? Guid.Empty : new Guid(OrderId);
            if (parsedOrderId == Guid.Empty)
                return HelperFunc.MyApiResponse(false, StatusCodes.Status404NotFound, "No Record Found", null);

            var request = mediaViewModel;
            if (request.Images.IsNullOrEmpty() || request.Images == null)
                return HelperFunc.MyApiResponse(false, StatusCodes.Status400BadRequest, "Invalid request!", null);

            var updateOrderStatus = await _context.Orders
                                                .Where(x => x.Id == parsedOrderId && x.IsAssigned && !x.IsCompleted)
                                                .FirstOrDefaultAsync();
            if (updateOrderStatus == null)
                return HelperFunc.MyApiResponse(false, StatusCodes.Status404NotFound, "Orders Files Already Uploaded With this Order Id!", null);

            var poNo = updateOrderStatus!.PoNo.ToString();

            var orderMediaList = await UploadAndSaveImagesAsync(request.Images, poNo, parsedOrderId);

            await _context.OrderMedias.AddRangeAsync(orderMediaList);

            var updateAssignedOrderStatus = await _context.AssignOrders
                                        .Where(x => x.OrderId == parsedOrderId && !x.IsCompleted)
                                        .FirstOrDefaultAsync();
            if (updateAssignedOrderStatus != null)
            {
                updateAssignedOrderStatus.EndTime = DateTime.Now;
                updateAssignedOrderStatus.IsCompleted = true;
            }

            if (updateOrderStatus != null)
                updateOrderStatus.IsCompleted = true;

            await _context.SaveChangesAsync();

            return HelperFunc.MyApiResponse(true, StatusCodes.Status200OK, "Files Uploaded Successfully!", null);
        }
        catch (Exception ex)
        {
            // Log the exception
            Console.WriteLine($"An error occurred: {ex.Message}");
            return HelperFunc.MyApiResponse(false, StatusCodes.Status500InternalServerError, $"An error occurred while fetching records. Inner Exception: {ex.Message}", null);
        }
    }

    private async Task<List<OrderMedia>> UploadAndSaveImagesAsync(IFormFileCollection? images, string downloadOrderNo, Guid orderId)
    {
        if (images == null || images.Count == 0)
            return new List<OrderMedia>();

        var (savedImagesPath, _) = await _imgHelper.UploadOrderFiles(images, downloadOrderNo);

        return savedImagesPath.Select(imagePath => new OrderMedia
        {
            ImageUrl = imagePath,
            IsUploadedByCustomer = false,            //must be false because it is uploaded by employee
            OrderId = orderId
        }).ToList();
    }

}
