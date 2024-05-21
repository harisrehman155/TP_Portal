using System.ComponentModel.DataAnnotations;

namespace TP_Portal.ViewModel;

public class AssignOrderToEmployeeViewModel
{
    public string? PoNo { get; set; }
    public DateTime Date { get; set; }
    public string? OrderName { get; set; }
    public string? OrderTypeId { get; set; }
    public string? OrderType { get; set; }
    public string? EmployeeId { get; set; }
    public string? OrderId { get; set; }

}