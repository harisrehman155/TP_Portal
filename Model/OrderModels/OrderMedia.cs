using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TP_Portal.Model.OrderModels
{
    public class OrderMedia
    {
        [Key]
        public Guid Id { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsUploadedByCustomer { get; set; }
        
        //Foreign Keys

        //Adding Foreign Key Constraints for Order
        public Guid OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order? Order { get; set; }
    }
}