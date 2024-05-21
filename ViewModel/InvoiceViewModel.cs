using Swashbuckle.AspNetCore;
using Swashbuckle.AspNetCore.Annotations;

namespace TP_Portal.ViewModel;


public class InvoiceViewModel
{
    public string? InvoiceId { get; set; }
    public string? InvoiceDate { get; set; }
    public string? InvoiceExpiryDate { get; set; }
    public string? Customer { get; set; }
    public string? Total { get; set; }
    public bool IsPaid { get; set; }
}
public class InvoiceWithDetailViewModel
{
    public string? InvoiceId { get; set; }
    public string? InvoiceDate { get; set; }
    public string? InvoiceExpiryDate { get; set; }
    public string? Customer { get; set; }
    public string? Total { get; set; }
    public bool IsPaid { get; set; }
    public List<OrderDetailViewModel>? OrderDetailsViewModel { get; set; }
}
public class OrderDetailViewModel
{
    public string? PoNo { get; set; }
    public string? Date { get; set; }
    public string? OrderName { get; set; }
    public string? OrderTypeName { get; set; }
    public string? DesignType { get; set; }
    public decimal OrderPrice { get; set; }
    public string? CustomerId { get; set; }

}

public class InvoiceRequest
{
    [SwaggerSchema(Nullable = true)]
    public string? InvoiceId { get; set; }

    [SwaggerSchema(Nullable = true)]
    public string? CustomerId { get; set; }

    [SwaggerSchema(Nullable = true)]
    public string? Month { get; set; }
}