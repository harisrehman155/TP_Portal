using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TP_Portal.Model.MyApplicationUser;

namespace TP_Portal.Context;

public interface ISeedData
{
    Task SeedDataAsync();
    Task SeedRoles();
    Task SeedOrderType();
    Task SeedAdminUser();

}
public class SeedData : ISeedData
{
    private readonly ApplicationDbContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public SeedData(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _roleManager = roleManager;
        _userManager = userManager;

    }

    public async Task SeedDataAsync()
    {
        await SeedRoles();
        await SeedAdminUser();
        await SeedOrderType();
    }

    public async Task SeedRoles()
    {
        // Seed roles
        var roles = new[]
        {
        new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "ADMIN" },
        new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "User", ConcurrencyStamp = "2", NormalizedName = "USER" },
        new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Digitizer", ConcurrencyStamp = "3", NormalizedName = "DIGITIZER" },
        new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "VectorArtist", ConcurrencyStamp = "4", NormalizedName = "VECTORARTIST" }
    };

        foreach (var role in roles)
        {
            if (!await _roleManager.RoleExistsAsync(role.Name))
            {
                await _roleManager.CreateAsync(role);
            }
        }
    }

    public async Task SeedOrderType()
    {
        // Seeding Order Types
        var orderTypes = new List<OrderType>
    {
        new OrderType { Id = Guid.NewGuid(), Name = "Vector" },
        new OrderType { Id = Guid.NewGuid(), Name = "Digitize" },
        new OrderType { Id = Guid.NewGuid(), Name = "Patch" }
    };

        foreach (var orderType in orderTypes)
        {
            if (!await _context.OrderTypes.AnyAsync(x => x.Name == orderType.Name))
            {
                await _context.OrderTypes.AddAsync(orderType);
            }
        }

        await _context.SaveChangesAsync(); // Save changes if any new order types were added
    }

    public async Task SeedAdminUser()
    {
        // Seed a new user
        string _username = "Haris155";
        string _email = "harisrehman155@gmail.com";
        var adminUser = new ApplicationUser
        {
            Id = "cf5edd27-228f-4db4-bffb-207469ce9893",
            UserName = _username,
            NormalizedUserName = _username.ToUpper(),
            Email = _email,
            NormalizedEmail = _email.ToUpper(),
            PhoneNumber = "03112640322",
            Address = "123 Main St",
            City = "Karachi",
            Company = "Terminator Punch",
            CompanyType = "Technology",
            CompanyWebsiteUrl = "https://www.terminatorpunch.com",
            IsActive = true,
            EmailConfirmed = true
        };
        var hasher = new PasswordHasher<ApplicationUser>();
        adminUser.PasswordHash = hasher.HashPassword(adminUser, "H@r!$125150");

        var user = await _userManager.FindByEmailAsync(_email);
        if (user == null)
        {
            await _userManager.CreateAsync(adminUser);
            await _userManager.AddToRoleAsync(adminUser, "Admin");
            await _userManager.AddToRoleAsync(adminUser, "User");
        }

        // No need to call _context.SaveChangesAsync() here, as the user manager handles saving changes for Identity
    }

}