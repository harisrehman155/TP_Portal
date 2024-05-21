using System.ComponentModel.DataAnnotations;

namespace TP_Portal.ViewModel;

public class ResetPasswordViewModel
{
    [EmailAddress]
    [Required(ErrorMessage = "Email is Required")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is Required")]
    public string? Password { get; set; }

    [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
    public string? ConfirmPassword { get; set; }

    [Required]
    public string? Token { get; set; }
}