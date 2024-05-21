using System.ComponentModel.DataAnnotations;

namespace TP_Portal.ViewModel;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Full Name is Required")]
    public string? Name { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Email is Required")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Phone Number is Required")]
    public string? Phone { get; set; }

    [Required(ErrorMessage = "Address is Required")]
    public string? Address { get; set; }
    public string? City { get; set; }

    [Required(ErrorMessage = "Company Name is Required")]
    public string? CompanyName { get; set; }
    public string CompanyWebsiteUrl { get; set; } = string.Empty;

    [Required(ErrorMessage = "Company Type is Required")]
    public string? CompanyType { get; set; }

    [Required(ErrorMessage = "Password is Required")]
    public string? Password { get; set; }

    [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
    public string? ConfirmPassword { get; set; }

}