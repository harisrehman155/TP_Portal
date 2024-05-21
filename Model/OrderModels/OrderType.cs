using System.ComponentModel.DataAnnotations;

namespace TP_Portal.Model.OrderModels
{
    public class OrderType
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
}