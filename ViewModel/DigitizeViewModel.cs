using System.ComponentModel.DataAnnotations;
namespace TP_Portal.ViewModel
{
    public class DigitizeBaseVM
    {
        [Required(ErrorMessage = "Order Name is Required")]
        public string? Name { get; set; }
        

        [Required(ErrorMessage = "Order Description is Required")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Fabric is Required")]
        public string? Fabric { get; set; }

        [Required(ErrorMessage = "Placement is Required")]
        public string? Placement { get; set; }

        public string? Height { get; set; }
        public string? Width { get; set; }
        public string? NoOfColor { get; set; }
        public bool IsUrgent { get; set; }
        
    }

    public class GetAllDigitizeOrdersVM : DigitizeBaseVM
    {
        public Guid Id { get; set; }
        public int PoNo { get; set; }
        public DateTime Date { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<MediaViewModel>? OrderMedia { get; set; }
    }

    public class CreateDigitizeOrderVM : DigitizeBaseVM
    {
        public MediaViewModel? OrderMedia { get; set; }
    }

    public class UpdateDigitizeOrderVM : DigitizeBaseVM
    {
        public Guid Id { get; set; }
        public MediaViewModel? OrderMedia { get; set; }
    }

    public class DeleteDigitizeOrderVM
    {
        public Guid Id { get; set; }
    }
}
