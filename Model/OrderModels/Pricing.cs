using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TP_Portal.Model.MyApplicationUser;

namespace TP_Portal.Model.OrderModels
{
    public class Pricing
    {
        [Key]
        public Guid Id { get; set; }
        public string? DesignTypeName { get; set; }
        public string? DesignPrice { get; set; }

        //Foreign Keys

        //Adding Foreign Key Constraints for OrderType
        public Guid? OrderTypeId { get; set; }
        [ForeignKey("OrderTypeId")]
        public OrderType? OrderType { get; set; }

        //Adding Foreign Key Constraints for User
        public string? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public ApplicationUser? User { get; set; } // Navigation property to represent the User relationship

    }
}