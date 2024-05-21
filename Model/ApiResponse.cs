
namespace TP_Portal.Model
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; } = true;
        public int Status { get; set; }
        public string? Message { get; set; }
        public object Response { get; set; } = new object();
        public List<string>? Errors { get; set; } = null;
    }
}