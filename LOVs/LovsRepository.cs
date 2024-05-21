using Microsoft.EntityFrameworkCore;
using TP_Portal.Context;
using TP_Portal.Helper;
using TP_Portal.ViewModel;
using TP_Portal.ViewModel.LOVsViewModel;

namespace TP_Portal.LOVs;

public interface ILOVsRepository
{
    Task<ApiResponse> GetAllDigitizersAsync();
    Task<ApiResponse> GetAllVectorArtistsAsync();
}

public class LOVsRepository : ILOVsRepository
{
    private readonly ApplicationDbContext _context;

    public LOVsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse> GetAllDigitizersAsync()
    {
        try
        {
            var result = await (
                from user in _context.Users
                join userRole in _context.UserRoles on user.Id equals userRole.UserId
                join role in _context.Roles on userRole.RoleId equals role.Id
                where role.Name == "Digitizer"
                select new AdminLovViewModel
                {
                    Id = user.Id,
                    Name = user.UserName
                }
            ).ToListAsync();

            return result != null && result.Any()
                ? HelperFunc.MyApiResponse(true, StatusCodes.Status200OK, "Records fetched successfully!", result)
                : HelperFunc.MyApiResponse(false, StatusCodes.Status404NotFound, "No records found!", null);
        }
        catch (Exception ex)
        {
            // Log the exception for debugging purposes
            return HelperFunc.MyApiResponse(
                false,
                StatusCodes.Status500InternalServerError,
                $"Exception occurred while fetching digitizers. Inner Exception: {ex.Message}",
                new { }
            );
        }
    }

    public async Task<ApiResponse> GetAllVectorArtistsAsync()
    {
        try
        {
            var result = await (
                from user in _context.Users
                join userRole in _context.UserRoles on user.Id equals userRole.UserId
                join role in _context.Roles on userRole.RoleId equals role.Id
                where role.Name == "VectorArtist"
                select new AdminLovViewModel
                {
                    Id = user.Id,
                    Name = user.UserName
                }
            ).ToListAsync();

            return result != null && result.Any()
                ? HelperFunc.MyApiResponse(true, StatusCodes.Status200OK, "Records fetched successfully!", result)
                : HelperFunc.MyApiResponse(false, StatusCodes.Status404NotFound, "No records found!", null);
        }
        catch (Exception ex)
        {
            // Log the exception for debugging purposes
            return HelperFunc.MyApiResponse(
                false,
                StatusCodes.Status500InternalServerError,
                $"Exception occurred while fetching digitizers. Inner Exception: {ex.Message}",
                new { }
            );
        }
    }
}
