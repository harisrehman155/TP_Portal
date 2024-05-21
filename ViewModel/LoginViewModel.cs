using System.ComponentModel.DataAnnotations;

namespace TP_Portal.ViewModel;

public class LoginViewModel
{
    [EmailAddress]
    [Required(ErrorMessage = "Email is Required")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is Required")]
    public string? Password { get; set; }

}