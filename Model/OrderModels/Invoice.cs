using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TP_Portal.Model.MyApplicationUser;

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

        //Adding Foreign Key Constraints for User
        public string? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public ApplicationUser? User { get; set; }
    }
}