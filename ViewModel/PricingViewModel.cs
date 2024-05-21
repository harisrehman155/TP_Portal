using System.ComponentModel.DataAnnotations;
namespace TP_Portal.ViewModel
{
    public class PricingBaseVM
    {
        [Required(ErrorMessage = "DesignType Name is Required")]
        public string? DesignTypeName { get; set; }
        
        [Required(ErrorMessage = "Design Price is Required")]
        public string? DesignPrice { get; set; }
        public string? OrderTypeId { get; set; }
        public string? CustomerId { get; set; }

    }

    public class GetAllPricingVM : PricingBaseVM
    {
        public Guid Id { get; set; }
        public string? OrderType { get; set; }
        public string? Customer { get; set; }
    }

    public class CreatePricingVM : PricingBaseVM
    {
    }

    public class UpdatePricingVM : PricingBaseVM
    {
        public Guid Id { get; set; }
    }

    public class DeletePricingVM
    {
        public Guid Id { get; set; }
    }
}
