using System.ComponentModel.DataAnnotations;

namespace TP_Portal.ViewModel;

public class AssignPricingToOrderVM
{
    public Guid Id { get; set; }
    public string? PoNo { get; set; }
    public DateTime Date { get; set; }
    public string? OrderName { get; set; }
    public string? OrderTypeId { get; set; }
    public string? OrderType { get; set; }
    public string? EmployeeId { get; set; }
    public string? OrderId { get; set; }
    public string? PricingId { get; set; }
    public string? CustomPrice { get; set; }

}