using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TP_Portal.Repositories;
using TP_Portal.ViewModel;

namespace TP_Portal.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{
    private readonly IAdminRepository _adminRepository;
    public AdminController(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }

    [HttpGet("GetUnAssignedRoleUsers")]
    public async Task<ActionResult<ApiResponse>> GetUnAssignedRoleUsers()
    {
        ApiResponse _response = await _adminRepository.GetUnAssignedRoleUsersAsync();
        return Ok(_response);
    }
    [HttpPost("AssignRoleToUsers")]
    public async Task<ActionResult<ApiResponse>> AssignRoleToUsers(List<AssignRoleToUsersViewModel> AssignRoleToUsersViewModels)
    {
        ApiResponse _response = await _adminRepository.AssignRoleToUsersAsync(AssignRoleToUsersViewModels);
        return Ok(_response);
    }

    [HttpGet("GetUnAssignedOrders")]
    public async Task<ActionResult<ApiResponse>> GetUnAssignedOrders()
    {
        ApiResponse _response = await _adminRepository.GetUnAssignedOrdersAsync();
        return Ok(_response);
    }

    [HttpPost("AssignOrdersToEmployee")]
    public async Task<ActionResult<ApiResponse>> AssignOrdersToEmployee(List<AssignOrderToEmployeeViewModel> assignOrderToEmployeeViewModel)
    {
        ApiResponse _response = await _adminRepository.AssignOrdersToEmployeeAsync(assignOrderToEmployeeViewModel);
        return Ok(_response);
    }

    #region Pricing

    [HttpGet("GetUnAssignedOrderForPricing")]
    public async Task<ActionResult<ApiResponse>> GetUnAssignedOrderForPricing()
    {
        ApiResponse _response = await _adminRepository.GetUnAssignedOrderForPricingAsync();
        return Ok(_response);
    }

    [HttpPost("SetOrderPricing")]
    public async Task<ActionResult<ApiResponse>> SetOrderPricing(List<AssignPricingToOrderVM> assignPricingToOrdersVM)
    {
        ApiResponse _response = await _adminRepository.SetOrderPricingAsync(assignPricingToOrdersVM);
        return Ok(_response);
    }

    #endregion

    [HttpPost("GetCustomerInvoicesWithDetail")]
    public async Task<ActionResult<ApiResponse>> GetCustomerInvoicesWithDetail([FromBody] InvoiceRequest invoiceRequest)
    {
        ApiResponse _response = await _adminRepository.GetCustomerInvoicesWithDetailAsync(invoiceRequest);
        return Ok(_response);
    }

    [HttpGet("GenerateCustomerInvoices")]
    public async Task<ActionResult<ApiResponse>> GenerateCustomerInvoices([Required][FromQuery] string month)
    {
        ApiResponse _response = await _adminRepository.GenerateCustomerInvoicesAsync(month);
        return Ok(_response);
    }

    [HttpGet("GenerateEmployeeInvoices")]
    public async Task<ActionResult<ApiResponse>> GenerateEmployeeInvoices([Required][FromQuery] string month)
    {
        ApiResponse _response = await _adminRepository.GenerateEmployeeInvoicesAsync(month);
        return Ok(_response);
    }

    [HttpGet("GetUnpaidCustomerInvoices")]
    public async Task<ActionResult<ApiResponse>> GetUnpaidCustomerInvoices([Required][FromQuery] string month)
    {
        ApiResponse _response = await _adminRepository.GetUnpaidCustomerInvoicesAsync(month);
        return Ok(_response);
    }

    [HttpPost("MarkCustomerInvoicesAsPaid")]
    public async Task<ActionResult<ApiResponse>> MarkCustomerInvoicesAsPaid([FromBody] List<InvoiceViewModel> invoiceViewModel)
    {
        ApiResponse _response = await _adminRepository.MarkCustomerInvoicesAsPaidAsync(invoiceViewModel);
        return Ok(_response);
    }
}
