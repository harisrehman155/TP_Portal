using Microsoft.AspNetCore.Identity;

namespace TP_Portal.Model.MyApplicationUser;

public class ApplicationUser : IdentityUser
{
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Company { get; set; }
    public string? CompanyType { get; set; }
    public string CompanyWebsiteUrl { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;
}