using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TP_Portal.Model.MyApplicationUser;

namespace TP_Portal.Model.OrderModels
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public int PoNo { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Height { get; set; }
        public string? Width { get; set; }
        public string? Placement { get; set; }
        public string? ColorType { get; set; }
        public string? NoOfColor { get; set; }
        public string? Fabric { get; set; }
        public bool IsUrgent { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsAssigned { get; set; }
        public bool IsGivenPriced { get; set; }
        public bool IsPaid { get; set; }
        public DateTime Date { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? OrderPrice { get; set; }


        //Foreign Keys

        // Adding Foreign Key Constraints for Invoice
        public Guid? InvoiceId { get; set; }
        [ForeignKey("InvoiceId")]
        public Invoice? Invoice { get; set; }

        //Adding Foreign Key Constraints for OrderType
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }
        
        //Adding Foreign Key Constraints for OrderType
        public Guid? OrderTypeId { get; set; }
        [ForeignKey("OrderTypeId")]
        public OrderType? OrderType { get; set; }

        //Adding Foreign Key Constraints for OrderType
        public List<OrderMedia>? OrderMedia { get; set; }
    }
}