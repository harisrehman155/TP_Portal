
using Microsoft.EntityFrameworkCore;
using TP_Portal.Context;

namespace TP_Portal.Helper;

public class MyHelperFunc
{
    private readonly ApplicationDbContext _context;

    public MyHelperFunc(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid?> GetOrderTypeIdAsync(string orderType)
    {
        try
        {
            // Retrieve the OrderType from the database by name
            var orderTypeEntity = await _context.OrderTypes
                .SingleOrDefaultAsync(x => x.Name == orderType);

            // If the OrderType exists, return its Id
            if (orderTypeEntity != null)
            {
                return orderTypeEntity.Id;
            }
            else
            {
                // If the OrderType doesn't exist, return null
                return null;
            }
        }
        catch (Exception ex)
        {
            // Log or handle the exception appropriately
            Console.WriteLine($"An error occurred while retrieving OrderType: {ex.Message}");
            return null;
        }
    }

}