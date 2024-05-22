using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TP_Portal.Context;
using TP_Portal.Helper;
using TP_Portal.Model.MyApplicationUser;
using TP_Portal.ViewModel;

namespace TP_Portal.Repositories;

public interface IAdminRepository
{
    Task<ApiResponse> GetUnAssignedRoleUsersAsync();
    Task<ApiResponse> GetUnAssignedOrdersAsync();
    Task<ApiResponse> GetUnAssignedOrderForPricingAsync();
    Task<ApiResponse> GetCustomerInvoicesWithDetailAsync(InvoiceRequest invoiceRequest);
    Task<ApiResponse> GetUnpaidCustomerInvoicesAsync(string month);
    Task<ApiResponse> GenerateCustomerInvoicesAsync(string month);
    Task<ApiResponse> GenerateEmployeeInvoicesAsync(string month);
    Task<ApiResponse> AssignRoleToUsersAsync(List<AssignRoleToUsersViewModel> _assignRoleToUsersViewModels);
    Task<ApiResponse> AssignOrdersToEmployeeAsync(List<AssignOrderToEmployeeViewModel> assignOrderToEmployeeViewModel);
    Task<ApiResponse> SetOrderPricingAsync(List<AssignPricingToOrderVM> assignPricingToOrderVM);
    Task<ApiResponse> MarkCustomerInvoicesAsPaidAsync(List<InvoiceViewModel> invoiceViewModel);
}
public class AdminRepository : IAdminRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _context;

    public AdminRepository(UserManager<ApplicationUser> userManager, ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _context = context;
        _roleManager = roleManager;
    }

    public async Task<ApiResponse> GetUnAssignedOrdersAsync()
    {
        try
        {
            var unassignedOrders = await _context.Orders
                .Include(order => order.OrderType)
                .Where(order => !order.IsCompleted && !order.IsAssigned)
                .Select(order => new AssignOrderToEmployeeViewModel
                {
                    PoNo = order.PoNo.ToString(),
                    Date = order.Date,
                    OrderName = order.Name,
                    OrderTypeId = order.OrderTypeId.ToString(),
                    OrderType = order.OrderType.Name,
                    EmployeeId = null,
                    OrderId = order.Id.ToString()
                })
                .OrderBy(x => x.PoNo)
                .ToListAsync();

            return unassignedOrders != null && unassignedOrders.Any()
                ? HelperFunc.MyApiResponse(true, StatusCodes.Status200OK, "Records fetched successfully!", unassignedOrders)
                : HelperFunc.MyApiResponse(false, StatusCodes.Status404NotFound, "No records found!", null);
        }
        catch (Exception ex)
        {
            return HelperFunc.MyApiResponse(false, StatusCodes.Status500InternalServerError, $"Exception occurred while fetching unassigned orders. Inner Exception: {ex.Message}", new { });
        }
    }

    public async Task<ApiResponse> AssignOrdersToEmployeeAsync(List<AssignOrderToEmployeeViewModel> assignOrderToEmployeeViewModel)
    {
        try
        {
            if (assignOrderToEmployeeViewModel != null && assignOrderToEmployeeViewModel.Any())
            {
                var ordersToAssign = assignOrderToEmployeeViewModel.Select(unAssignOrder =>
                    new AssignOrder
                    {
                        Id = Guid.NewGuid(),
                        StartTime = DateTime.Now,
                        OrderId = string.IsNullOrEmpty(unAssignOrder.OrderId) ? Guid.Empty : new Guid(unAssignOrder.OrderId),
                        EmployeeId = unAssignOrder.EmployeeId
                    }
                ).ToList();

                await _context.AssignOrders.AddRangeAsync(ordersToAssign);

                foreach (var order in ordersToAssign)
                {
                    var orderToUpdate = await _context.Orders.FirstOrDefaultAsync(x => x.Id == order.OrderId);
                    if (orderToUpdate != null)
                    {
                        orderToUpdate.IsAssigned = true;
                    }
                }

                await _context.SaveChangesAsync();

                return HelperFunc.MyApiResponse(true, StatusCodes.Status200OK, "Orders assigned successfully!", new { });
            }
            else
            {
                return HelperFunc.MyApiResponse(false, StatusCodes.Status400BadRequest, "No orders provided for assignment!", new { });
            }
        }
        catch (Exception ex)
        {
            return HelperFunc.MyApiResponse(false, StatusCodes.Status500InternalServerError, $"Exception occurred while assigning orders. Inner Exception: {ex.Message}", new { });
        }
    }

    public async Task<ApiResponse> GetUnAssignedRoleUsersAsync()
    {
        try
        {
            var result = await (
                from _user in _context.Users
                where !_user.IsActive
                select new AssignRoleToUsersViewModel
                {
                    Id = _user.Id,
                    Name = _user.UserName,
                    Email = _user.Email,
                    IsActive = _user.IsActive
                }
            ).ToListAsync();

            return result != null && result.Any()
                ? HelperFunc.MyApiResponse(true, StatusCodes.Status200OK, "Records fetched successfully!", result)
                : HelperFunc.MyApiResponse(false, StatusCodes.Status404NotFound, "No records found!", null);
        }
        catch (Exception ex)
        {
            return HelperFunc.MyApiResponse(false, StatusCodes.Status500InternalServerError, $"Exception occurred while fetching unassigned users. Inner Exception: {ex.Message}", new { });
        }
    }

    public async Task<ApiResponse> AssignRoleToUsersAsync(List<AssignRoleToUsersViewModel> _assignRoleToUsersViewModels)
    {
        try
        {
            if (_assignRoleToUsersViewModels == null || !_assignRoleToUsersViewModels.Any())
            {
                return HelperFunc.MyApiResponse(false, StatusCodes.Status400BadRequest, "No roles provided for assignment!", new { });
            }

            foreach (var UserWithRole in _assignRoleToUsersViewModels)
            {
                var _roleExists = await _roleManager.FindByIdAsync(UserWithRole.RoleId.ToString());
                if (_roleExists != null)
                {
                    var user = await _userManager.FindByEmailAsync(UserWithRole.Email);
                    if (user != null)
                    {
                        await _userManager.AddToRoleAsync(user, _roleExists.Name);
                        user.IsActive = true;
                        await _userManager.UpdateAsync(user);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        return HelperFunc.MyApiResponse(false, StatusCodes.Status404NotFound, $"User with email '{UserWithRole.Email}' not found!", new { });
                    }
                }
                else
                {
                    return HelperFunc.MyApiResponse(false, StatusCodes.Status404NotFound, $"Role with ID '{UserWithRole.RoleId}' not found!", new { });
                }
            }
            return HelperFunc.MyApiResponse(true, StatusCodes.Status200OK, "Roles updated successfully!", new { });
        }
        catch (Exception ex)
        {
            return HelperFunc.MyApiResponse(false, StatusCodes.Status500InternalServerError, $"Exception occurred while assigning roles. Inner Exception: {ex.Message}", new { });
        }
    }

    public async Task<ApiResponse> GetUnAssignedOrderForPricingAsync()
    {
        try
        {
            var unassignedOrdersForPricing = await _context.AssignOrders
                .Include(o => o.Order)
                .Include(p => p.Pricing)
                .Where(x => !x.Order.IsGivenPriced && x.Order.IsCompleted)
                .Select(o => new AssignPricingToOrderVM
                {
                    Id = o.Id,
                    PoNo = o.Order.PoNo.ToString(),
                    Date = o.Order.Date,
                    OrderName = o.Order.Name,
                    OrderTypeId = o.Order.OrderTypeId.ToString(),
                    OrderType = o.Order.OrderType.Name,
                    EmployeeId = o.EmployeeId,
                    OrderId = o.OrderId.ToString(),
                    PricingId = ""
                })
                .OrderBy(x => x.PoNo)
                .ToListAsync();

            return unassignedOrdersForPricing.Any()
                ? HelperFunc.MyApiResponse(true, StatusCodes.Status200OK, "Records fetched successfully!", unassignedOrdersForPricing)
                : HelperFunc.MyApiResponse(false, StatusCodes.Status404NotFound, "No records found!", null);
        }
        catch (Exception ex)
        {
            return HelperFunc.MyApiResponse(false, StatusCodes.Status500InternalServerError, $"Exception occurred while fetching unassigned orders. Inner Exception: {ex.Message}", new { });
        }
    }

    public async Task<ApiResponse> SetOrderPricingAsync(List<AssignPricingToOrderVM> assignPricingToOrdersVM)
    {
        try
        {
            var response = assignPricingToOrdersVM;
            if (response == null || !response.Any())
            {
                return HelperFunc.MyApiResponse(false, StatusCodes.Status400BadRequest, "No orders provided for assignment!", new { });
            }

            var assignOrderRecords = await _context.AssignOrders.ToListAsync();
            var pricingTypeRecords = await _context.Pricing.ToListAsync();
            foreach (var order in response)
            {
                if (string.IsNullOrEmpty(order.CustomPrice) && string.IsNullOrEmpty(order.PricingId))
                    return HelperFunc.MyApiResponse(false, StatusCodes.Status400BadRequest, "No Pricing was Selected!", new { });

                var myOrder = await _context.Orders.Where(x => x.Id == new Guid(order.OrderId!)).FirstOrDefaultAsync();
                var assignOrderRecord = assignOrderRecords.Where(x => x.Id == order.Id).FirstOrDefault();
                var customerPricing = await _context.Pricing.Where(x => x.Id == new Guid(order.PricingId!)).FirstOrDefaultAsync();

                if (assignOrderRecord != null && customerPricing != null && myOrder != null)
                {
                    var employeePrincingType = pricingTypeRecords
                            .Where(x => x.OrderTypeId == customerPricing!.OrderTypeId && x.CustomerId == order.EmployeeId)
                            .FirstOrDefault();


                    myOrder.IsGivenPriced = true;
                    assignOrderRecord.PricingId = new Guid(order.PricingId);
                    assignOrderRecord.EmployeePricingId = customerPricing.Id;
                    assignOrderRecord.IsPaidToEmployee = true;
                    myOrder.OrderPrice = string.IsNullOrEmpty(order.CustomPrice) ? customerPricing.DesignPrice : order.CustomPrice;
                }
                else
                {
                    return HelperFunc.MyApiResponse(false, StatusCodes.Status404NotFound, $"Pricing not found!", new { });
                }
            }
            await _context.SaveChangesAsync();

            return HelperFunc.MyApiResponse(true, StatusCodes.Status200OK, "Pricing Assigned successfully!", new { });
        }
        catch (Exception ex)
        {
            return HelperFunc.MyApiResponse(false, StatusCodes.Status500InternalServerError, $"Exception occurred while assigning roles. Inner Exception: {ex.Message}", new { });
        }
    }

    public async Task<ApiResponse> GetCustomerInvoicesWithDetailAsync(InvoiceRequest invoiceRequest)
    {
        try
        {
            // Determine the start and end date based on the provided month or current month
            DateTime startDate;
            DateTime endDate;
            if (!string.IsNullOrEmpty(invoiceRequest.Month))
            {
                startDate = DateTime.ParseExact(invoiceRequest.Month, "MMMM-yyyy", CultureInfo.InvariantCulture);
                endDate = startDate.AddMonths(1).AddDays(-1);
            }
            else
            {
                startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                endDate = startDate.AddMonths(1).AddDays(-1);
            }

            var query = from invoice in _context.Invoice
                        join order in _context.Orders on invoice.Id equals order.InvoiceId
                        join assignOrder in _context.AssignOrders on order.Id equals assignOrder.OrderId
                        join price in _context.Pricing on assignOrder.PricingId equals price.Id
                        join user in _context.Users on order.UserId equals user.Id
                        join orderType in _context.OrderTypes on order.OrderTypeId equals orderType.Id
                        where (string.IsNullOrEmpty(invoiceRequest.CustomerId) || order.UserId == invoiceRequest.CustomerId)
                              && (string.IsNullOrEmpty(invoiceRequest.InvoiceId) || invoice.Id.ToString() == invoiceRequest.InvoiceId)
                              && (invoice.InvoiceDate >= startDate && invoice.InvoiceDate <= endDate)
                        group new { invoice, order, price, user, orderType } by new
                        {
                            invoice.Id,
                            invoice.InvoiceDate,
                            invoice.InvoiceExpiryDate,
                            invoice.IsPaid,
                            invoice.Total,
                            user.UserName
                        } into groupedInvoices
                        select new InvoiceWithDetailViewModel
                        {
                            InvoiceId = groupedInvoices.Key.Id.ToString(),
                            Customer = groupedInvoices.Key.UserName,
                            InvoiceDate = groupedInvoices.Key.InvoiceDate.ToString("dd-MMM-yyyy"),
                            InvoiceExpiryDate = groupedInvoices.Key.InvoiceExpiryDate.ToString("dd-MMM-yyyy"),
                            IsPaid = groupedInvoices.Key.IsPaid,
                            Total = groupedInvoices.Key.Total,
                            OrderDetailsViewModel = groupedInvoices.Select(g => new OrderDetailViewModel
                            {
                                PoNo = g.order.PoNo.ToString(),
                                CustomerId = g.order.UserId,
                                Date = g.order.Date.ToString("dd-MMM-yyyy"),
                                DesignType = g.price.DesignTypeName,
                                OrderName = g.order.Name,
                                OrderPrice = Convert.ToDecimal(g.order.OrderPrice),
                                OrderTypeName = g.orderType.Name
                            }).ToList()
                        };

            var invoices = await query.ToListAsync();

            return invoices.Any()
                ? HelperFunc.MyApiResponse(true, StatusCodes.Status200OK, "Records fetched successfully!", invoices)
                : HelperFunc.MyApiResponse(false, StatusCodes.Status404NotFound, "No records found!", null);
        }
        catch (Exception ex)
        {
            return HelperFunc.MyApiResponse(false, StatusCodes.Status500InternalServerError, $"Exception occurred while fetching invoices. Inner Exception: {ex.Message}", new { });
        }
    }

    public async Task<ApiResponse> GenerateCustomerInvoicesAsync(string month)
    {
        try
        {
            // Parse the month string to a DateTime object representing the start of the month
            var startDate = DateTime.ParseExact(month, "MMMM-yyyy", CultureInfo.InvariantCulture);
            // Calculate the end of the month
            var endDate = startDate.AddMonths(1).AddDays(-1);

            // Check if there are any orders in the given month whose price is not given
            var ordersWithMissingPrices = await _context.Orders
                .Where(order => !order.IsGivenPriced && order.IsCompleted && !order.IsPaid && order.IsAssigned &&
                    order.InvoiceId == null && (order.Date >= startDate && order.Date <= endDate))
                .AnyAsync();

            if (ordersWithMissingPrices)
            {
                return HelperFunc.MyApiResponse(false, StatusCodes.Status400BadRequest, "Some orders in the given month do not have prices assigned.", null);
            }

            // Fetch all unpaid orders in the given month
            var unPaidOrders = await (
                from order in _context.Orders
                join assignOrder in _context.AssignOrders on order.Id equals assignOrder.OrderId
                join orderType in _context.OrderTypes on order.OrderTypeId equals orderType.Id
                join price in _context.Pricing on assignOrder.PricingId equals price.Id
                where order.IsCompleted && !order.IsPaid && order.IsGivenPriced && order.IsAssigned
                      && order.InvoiceId == null && order.Date >= startDate && order.Date <= endDate
                select new
                {
                    order,
                    orderType,
                    price,
                    assignOrder
                }).ToListAsync();

            if (!unPaidOrders.Any())
                return HelperFunc.MyApiResponse(false, StatusCodes.Status404NotFound, "No records found!", null);

            var invoices = new List<Invoice>();

            // Group orders by CustomerId
            var groupedOrders = unPaidOrders.GroupBy(x => x.order.UserId);

            foreach (var group in groupedOrders)
            {
                var customerId = group.Key;
                var orderRecords = group.Select(x => x.order).ToList();

                var total = orderRecords.Sum(x => Convert.ToDecimal(x.OrderPrice));

                var invoice = new Invoice
                {
                    Id = Guid.NewGuid(),
                    InvoiceDate = DateTime.Now,
                    InvoiceExpiryDate = DateTime.Now.AddDays(7),
                    IsPaid = false,
                    Total = total.ToString(),
                    CustomerId = customerId // Assign CustomerId to the invoice if needed
                };

                // Update the InvoiceId for each order
                foreach (var order in orderRecords)
                {
                    order.InvoiceId = invoice.Id;
                }

                invoices.Add(invoice);
            }

            // Add invoices to the context
            await _context.Invoice.AddRangeAsync(invoices);

            // Save changes to the orders and invoices
            await _context.SaveChangesAsync();

            return HelperFunc.MyApiResponse(true, StatusCodes.Status200OK, "Invoices generated successfully!", null);
        }
        catch (Exception ex)
        {
            return HelperFunc.MyApiResponse(false, StatusCodes.Status500InternalServerError, $"Exception occurred while generating invoices. Inner Exception: {ex.Message}", new { });
        }
    }

    public async Task<ApiResponse> GenerateEmployeeInvoicesAsync(string month)
    {
        try
        {
            // Parse the month string to a DateTime object representing the start of the month
            var startDate = DateTime.ParseExact(month, "MMMM-yyyy", CultureInfo.InvariantCulture);
            // Calculate the end of the month
            var endDate = startDate.AddMonths(1).AddDays(-1);

            // Check if there are any orders in the given month whose price is not given
            var ordersWithMissingPrices = await _context.Orders
                .Where(order => !order.IsGivenPriced && order.IsCompleted && !order.IsPaid && order.IsAssigned &&
                    order.InvoiceId == null && (order.Date >= startDate && order.Date <= endDate))
                .AnyAsync();

            if (ordersWithMissingPrices)
            {
                return HelperFunc.MyApiResponse(false, StatusCodes.Status400BadRequest, "Some orders in the given month do not have prices assigned.", null);
            }

            // Fetch all unpaid orders in the given month
            var unPaidOrders = await (
                from assignOrder in _context.AssignOrders
                join price in _context.Pricing on assignOrder.EmployeePricingId equals price.Id
                join order in _context.Orders on assignOrder.OrderId equals order.Id
                join orderType in _context.OrderTypes on order.OrderTypeId equals orderType.Id
                where assignOrder.IsPaidToEmployee && order.Date >= startDate && order.Date <= endDate
                select new
                {
                    order,
                    orderType,
                    price,
                    assignOrder
                }).ToListAsync();

            if (!unPaidOrders.Any())
                return HelperFunc.MyApiResponse(false, StatusCodes.Status404NotFound, "No records found!", null);

            var invoices = new List<Invoice>();

            // Group orders by EmployeeId
            var groupedOrders = unPaidOrders.GroupBy(x => x.assignOrder.EmployeeId);

            foreach (var group in groupedOrders)
            {
                var EmployeeId = group.Key;
                var orderRecords = group.Select(x => x.price).ToList();

                var total = orderRecords.Sum(x => Convert.ToDecimal(x.DesignPrice));

                var invoice = new Invoice
                {
                    Id = Guid.NewGuid(),
                    InvoiceDate = DateTime.Now,
                    InvoiceExpiryDate = DateTime.Now.AddDays(7),
                    IsPaid = false,
                    Total = total.ToString(),
                    CustomerId = EmployeeId // Assign CustomerId to the invoice if needed
                };

                invoices.Add(invoice);
            }

            // Add invoices to the context
            await _context.Invoice.AddRangeAsync(invoices);

            // Save changes to the orders and invoices
            await _context.SaveChangesAsync();

            return HelperFunc.MyApiResponse(true, StatusCodes.Status200OK, "Invoices generated successfully!", null);
        }
        catch (Exception ex)
        {
            return HelperFunc.MyApiResponse(false, StatusCodes.Status500InternalServerError, $"Exception occurred while generating invoices. Inner Exception: {ex.Message}", new { });
        }
    }

    public async Task<ApiResponse> GetUnpaidCustomerInvoicesAsync(string month)
    {
        try
        {
            // Determine the start and end date based on the provided month or current month
            DateTime startDate = DateTime.ParseExact(month, "MMMM-yyyy", CultureInfo.InvariantCulture);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            var query = from invoice in _context.Invoice
                        join order in _context.Orders on invoice.Id equals order.InvoiceId
                        join user in _context.Users on order.UserId equals user.Id
                        where !invoice.IsPaid && (invoice.InvoiceDate >= startDate && invoice.InvoiceDate <= endDate)
                        group new { invoice, order, user } by new
                        {
                            invoice.Id,
                            invoice.InvoiceDate,
                            invoice.InvoiceExpiryDate,
                            invoice.IsPaid,
                            invoice.Total,
                            user.UserName
                        } into groupedInvoices
                        select new InvoiceViewModel
                        {
                            InvoiceId = groupedInvoices.Key.Id.ToString(),
                            Customer = groupedInvoices.Key.UserName,
                            InvoiceDate = groupedInvoices.Key.InvoiceDate.ToString("dd-MMM-yyyy"),
                            InvoiceExpiryDate = groupedInvoices.Key.InvoiceExpiryDate.ToString("dd-MMM-yyyy"),
                            IsPaid = groupedInvoices.Key.IsPaid,
                            Total = groupedInvoices.Key.Total,
                        };

            var invoices = await query.ToListAsync();

            return invoices.Any()
                ? HelperFunc.MyApiResponse(true, StatusCodes.Status200OK, "Records fetched successfully!", invoices)
                : HelperFunc.MyApiResponse(false, StatusCodes.Status404NotFound, "No records found!", null);
        }
        catch (Exception ex)
        {
            return HelperFunc.MyApiResponse(false, StatusCodes.Status500InternalServerError, $"Exception occurred while fetching invoices. Inner Exception: {ex.Message}", new { });
        }
    }

    public async Task<ApiResponse> MarkCustomerInvoicesAsPaidAsync(List<InvoiceViewModel> invoiceViewModel)
    {
        try
        {
            var invoicesToUpdate = new List<Invoice>();
            var responseMessages = new List<string>();

            foreach (var invoiceRequest in invoiceViewModel)
            {
                if (string.IsNullOrEmpty(invoiceRequest.InvoiceId))
                {
                    responseMessages.Add($"InvoiceId is missing!");
                    continue;
                }

                if (!Guid.TryParse(invoiceRequest.InvoiceId, out var invoiceId))
                {
                    responseMessages.Add($"Invalid InvoiceId format: {invoiceRequest.InvoiceId}");
                    continue;
                }

                var invoiceToBeUpdated = await _context.Invoice
                    .Include(o => o.Orders)
                    .FirstOrDefaultAsync(x => x.Id == invoiceId && !x.IsPaid);

                if (invoiceToBeUpdated == null)
                {
                    responseMessages.Add($"Invoice not found or already paid with this InvoiceId: {invoiceRequest.InvoiceId}");
                    continue;
                }

                invoiceToBeUpdated.IsPaid = true;
                foreach (var order in invoiceToBeUpdated.Orders)
                {
                    order.IsPaid = true;
                }

                invoicesToUpdate.Add(invoiceToBeUpdated);
            }

            if (invoicesToUpdate.Any())
            {
                await _context.SaveChangesAsync();
                responseMessages.Add("Invoices marked as paid successfully.");
                return HelperFunc.MyApiResponse(true, StatusCodes.Status200OK, "Operation completed successfully.", responseMessages);
            }

            return HelperFunc.MyApiResponse(false, StatusCodes.Status404NotFound, "No invoices were updated.", responseMessages);
        }
        catch (Exception ex)
        {
            return HelperFunc.MyApiResponse(false, StatusCodes.Status500InternalServerError, $"Exception occurred while marking invoices as paid. Inner Exception: {ex.Message}", null);
        }
    }


}
