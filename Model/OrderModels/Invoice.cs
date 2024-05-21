using System.ComponentModel.DataAnnotations;

namespace TP_Portal.Model.OrderModels
{
    public class Invoice
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime InvoiceExpiryDate { get; set; }
        public string? Total { get; set; }
        public bool IsPaid { get; set; }

        //Foreign Keys
        public ICollection<Order>? Orders { get; set; }
    }
}