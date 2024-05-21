using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TP_Portal.Model.MyApplicationUser;

namespace TP_Portal.Model.OrderModels
{
    public class AssignOrder
    {
        [Key]
        public Guid Id { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsCompleted { get; set; }

        //Foreign Keys
        public string? EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public ApplicationUser? User { get; set; } // Navigation property to represent the User relationship

        public Guid OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order? Order { get; set; }

        public Guid? PricingId { get; set; }
        [ForeignKey("PricingId")]
        public Pricing? Pricing { get; set; }

    }
}
