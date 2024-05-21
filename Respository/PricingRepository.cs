
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TP_Portal.Context;
using TP_Portal.Helper;
using TP_Portal.Model.MyApplicationUser;
using TP_Portal.ViewModel;


namespace TP_Portal.Repositories;

public interface IPricingRepository
{
    Task<ApiResponse> GetAllPricingAsync(string CustomerId);
    Task<ApiResponse> CreatePricingAsync(CreatePricingVM createPricingVM);
    Task<ApiResponse> UpdatePricingAsync(UpdatePricingVM updatePricingVM);
    Task<ApiResponse> DeletePricingAsync(string pricingId);
}
public class PricingRepository : IPricingRepository
{
    private readonly ApplicationDbContext _context;

    public PricingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    private ApiResponse UnauthorizedResponse()
    {
        return HelperFunc.MyApiResponse(false, StatusCodes.Status401Unauthorized, "Unauthorized access!", null);
    }

    public async Task<ApiResponse> GetAllPricingAsync(string CustomerId)
    {
        try
        {
            // Parse orderId to Guid
            Guid parsedCustomerId = string.IsNullOrEmpty(CustomerId) ? Guid.Empty : new Guid(CustomerId);

            // Retrieve digitized orders with the specified UserId, OrderId, and OrderTypeId
            var PricingRecords = await _context.Pricing
                .Include(u => u.User)
                .Where(x => (parsedCustomerId == Guid.Empty || x.Id == parsedCustomerId))
                .Select(p => new GetAllPricingVM
                {
                    Id = p.Id,
                    DesignTypeName = p.DesignTypeName,
                    DesignPrice = p.DesignPrice,
                    CustomerId = p.CustomerId,
                    Customer = p.User.UserName,
                    OrderTypeId = p.OrderTypeId.ToString(),
                    OrderType = p.OrderType.Name
                })
                .ToListAsync();

            if (PricingRecords.Any())
                return HelperFunc.MyApiResponse(true, StatusCodes.Status200OK, "Records fetched successfully!", PricingRecords);
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


    public async Task<ApiResponse> CreatePricingAsync(CreatePricingVM createPricingVM)
    {
        try
        {
            var request = createPricingVM;
            if (request == null)
                return HelperFunc.MyApiResponse(false, StatusCodes.Status400BadRequest, "Invalid request!", null);

            // Create a new order
            var pricing = new Pricing
            {
                Id = Guid.NewGuid(),
                DesignTypeName = request.DesignTypeName,
                DesignPrice = request.DesignPrice,
                CustomerId = request.CustomerId,
                OrderTypeId = string.IsNullOrEmpty(request.OrderTypeId) ? Guid.Empty : new Guid(request.OrderTypeId)
            };

            // Add the new order to the database
            _context.Pricing.Add(pricing);
            await _context.SaveChangesAsync();

            return HelperFunc.MyApiResponse(true, StatusCodes.Status201Created, "Record Created Successfully!", null);
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


    public async Task<ApiResponse> UpdatePricingAsync(UpdatePricingVM updatePricingVM)
    {
        try
        {
            var request = updatePricingVM;
            if (request == null)
                return HelperFunc.MyApiResponse(false, StatusCodes.Status400BadRequest, "Invalid request!", null);

            var pricingRecord = await _context.Pricing.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (pricingRecord == null)
                return HelperFunc.MyApiResponse(false, StatusCodes.Status404NotFound, "Record not found!", null);

            pricingRecord.DesignTypeName = request.DesignTypeName;
            pricingRecord.DesignPrice = request.DesignPrice;
            pricingRecord.CustomerId = request.CustomerId;
            pricingRecord.OrderTypeId = string.IsNullOrEmpty(request.OrderTypeId) ? Guid.Empty : new Guid(request.OrderTypeId);

            await _context.SaveChangesAsync();

            return HelperFunc.MyApiResponse(true, StatusCodes.Status200OK, "Record updated successfully!", null);
        }
        catch (Exception ex)
        {
            return HelperFunc.MyApiResponse(false, StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}", null);
        }
    }

    public async Task<ApiResponse> DeletePricingAsync(string pricingId)
    {
        try
        {
            Guid parsedPricingId = string.IsNullOrEmpty(pricingId) ? Guid.Empty : new Guid(pricingId);

            var PricingRecord = await _context.Pricing
                .FirstOrDefaultAsync(x => x.Id == parsedPricingId);
            if (PricingRecord != null)
            {
                _context.Pricing.Remove(PricingRecord);
                await _context.SaveChangesAsync();
                return HelperFunc.MyApiResponse(true, StatusCodes.Status200OK, "Record deleted successfully!", null);
            }
            else
            {
                return HelperFunc.MyApiResponse(false, StatusCodes.Status404NotFound, "No Record found!", null);
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

}
