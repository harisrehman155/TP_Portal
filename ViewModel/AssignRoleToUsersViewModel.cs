using System.ComponentModel.DataAnnotations;

namespace TP_Portal.ViewModel;

public class AssignRoleToUsersViewModel
{
    public string? Id { get; set; }
    [Required(ErrorMessage = "Full Name is Required")]
    public string? Name { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Email is Required")]
    public string? Email { get; set; }

    public bool IsActive { get; set; }
    public Guid RoleId { get; set; }

}