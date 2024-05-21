using System.ComponentModel.DataAnnotations;
namespace TP_Portal.ViewModel
{
    public class VectorBaseVM
    {
        [Required(ErrorMessage = "Order Name is Required")]
        public string? Name { get; set; }
        

        [Required(ErrorMessage = "Order Description is Required")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "ColorType is Required")]
        public string? ColorType { get; set; }
        public string? NoOfColor { get; set; }
        public bool IsUrgent { get; set; }
        
    }

    public class GetAllVectorOrdersVM : VectorBaseVM
    {
        public Guid Id { get; set; }
        public int PoNo { get; set; }
        public DateTime Date { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<MediaViewModel>? OrderMedia { get; set; }
    }

    public class CreateVectorOrderVM : VectorBaseVM
    {
        public MediaViewModel? OrderMedia { get; set; }
    }

    public class UpdateVectorOrderVM : VectorBaseVM
    {
        public Guid Id { get; set; }
        public MediaViewModel? OrderMedia { get; set; }
    }

    public class DeleteVectorOrderVM
    {
        public Guid Id { get; set; }
    }
}
