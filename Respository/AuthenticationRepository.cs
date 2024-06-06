
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TP_Portal.Helper;
using TP_Portal.Model.Email;
using TP_Portal.Model.MyApplicationUser;
using TP_Portal.Services.Email;
using TP_Portal.ViewModel;

namespace TP_Portal.Repositories;

public interface IAuthenticationRepository
{
    Task<ApiResponse> RegisterUserAsync(RegisterViewModel registerViewModel, string baseUrl);
    Task<ApiResponse> LoginUserAsync(LoginViewModel loginViewModel);
    Task<ApiResponse> LogoutUserAsync();
    Task<ApiResponse> ConfirmEmailAsync(string token, string email, string baseUrl);
    Task<ApiResponse> ForgotPasswordAsync(string email, string baseUrl);
    Task<ApiResponse> ResetPasswordAsync(string email, string token);
    Task<ApiResponse> ResetPasswordAsync(ResetPasswordViewModel resetPasswordViewModel);
}

public class AuthenticationRepository : IAuthenticationRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthenticationRepository(UserManager<ApplicationUser> userManager, IConfiguration configuration, IEmailService emailService, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _configuration = configuration;
        _emailService = emailService;
        _signInManager = signInManager;
    }

    public async Task<ApiResponse> LoginUserAsync(LoginViewModel loginViewModel)
    {
        try
        {
            var userToBeLogin = await _userManager.FindByEmailAsync(loginViewModel.Email!);

            if (userToBeLogin == null || !await _userManager.CheckPasswordAsync(userToBeLogin, loginViewModel.Password!))
            {
                return HelperFunc.MyApiResponse(false, StatusCodes.Status400BadRequest, "Invalid email or password", new { });
            }
            if (!userToBeLogin.IsActive)
            {
                return HelperFunc.MyApiResponse(false, StatusCodes.Status400BadRequest, "User Not Active, Contact your Adminsitrator!", new { });
            }
            if (!userToBeLogin.EmailConfirmed)
            {
                return HelperFunc.MyApiResponse(false, StatusCodes.Status400BadRequest, "Email Not Confirmed, Kindly Confirm your Email!", new { });
            }

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userToBeLogin.Id),
                    new Claim(ClaimTypes.Name,userToBeLogin.UserName!),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };

            var userRoles = await _userManager.GetRolesAsync(userToBeLogin);
            authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var jwtToken = HelperFunc.GetToken(authClaims, _configuration);
            var _token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return HelperFunc.MyApiResponse(
                true, StatusCodes.Status200OK, "Token Generated Successfully!", new
                {
                    token = _token,
                    expiration = jwtToken.ValidTo
                });
        }
        catch (Exception ex)
        {
            return HelperFunc.MyApiResponse(false, StatusCodes.Status500InternalServerError, $"Exception Occured, While Creating User. Inner Exception : {ex.Message}", new { });
        }
    }

    public async Task<ApiResponse> LogoutUserAsync()
    {
        try
        {
            await _signInManager.SignOutAsync();
            return HelperFunc.MyApiResponse(true, StatusCodes.Status200OK, "User Logout Successfully!", null);
        }
        catch (Exception ex)
        {
            return HelperFunc.MyApiResponse(false, StatusCodes.Status500InternalServerError, $"Exception Occured, While Creating User. Inner Exception : {ex.Message}", new { });
        }
    }

    public async Task<ApiResponse> RegisterUserAsync(RegisterViewModel registerViewModel, string baseUrl)
    {
        try
        {
            var _userToBeRegister = await _userManager.FindByEmailAsync(registerViewModel.Email!);
            if (_userToBeRegister == null)
            {
                var _newUser = new ApplicationUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = registerViewModel.Name,
                    Email = registerViewModel.Email,
                    Address = registerViewModel.Address,
                    City = registerViewModel.City,
                    Company = registerViewModel.CompanyName,
                    CompanyType = registerViewModel.CompanyType,
                    PhoneNumber = registerViewModel.Phone
                };
                var _user = await _userManager.CreateAsync(_newUser, registerViewModel.Password!);
                if (_user.Succeeded)
                {
                    // //Add token to verify the email
                    // var token = await _userManager.GenerateEmailConfirmationTokenAsync(_newUser);

                    // // Construct confirmation link
                    // var confirmationLink = $"{baseUrl.TrimEnd('/')}/authentication/confirmemail?token={Uri.EscapeDataString(token)}&email={Uri.EscapeDataString(_newUser.Email!)}";

                    // var message = new Message(new string[] { _newUser.Email! }, "Confirmation Email", confirmationLink!);
                    // _emailService.SendEmail(message);

                    GenerateEmailConfirmationToken(_newUser, baseUrl);

                    return HelperFunc.MyApiResponse(true, StatusCodes.Status201Created, "User Created and email sent, Successfully", new { });
                }
                else
                {
                    var errors = _user.Errors.Select(e => e.Description).ToList();
                    return HelperFunc.MyApiResponse(false, StatusCodes.Status400BadRequest, "User Failed to Create", new { }, errors);
                }
            }
            return HelperFunc.MyApiResponse(false, StatusCodes.Status400BadRequest, "Email Already Exists!", new { });
        }
        catch (Exception ex)
        {
            return HelperFunc.MyApiResponse(false, StatusCodes.Status500InternalServerError, $"Exception Occured, While Creating User. Inner Exception : {ex.Message}", new { });
        }
    }

    public async Task<ApiResponse> ConfirmEmailAsync(string token, string email, string baseUrl)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null && !await _userManager.IsEmailConfirmedAsync(user))
        {
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return HelperFunc.MyApiResponse(true, StatusCodes.Status200OK, "Email Verified Successfully", new { });
            }
            else if (result.Errors.Count() > 0)
            {
                GenerateEmailConfirmationToken(user, baseUrl);
                return HelperFunc.MyApiResponse(true, StatusCodes.Status200OK, "Token Expired, New Link has been sent to Email", new { });
            }
        }
        return HelperFunc.MyApiResponse(true, StatusCodes.Status200OK, "Email Already Verified!", new { });
    }

    public async Task<ApiResponse> ForgotPasswordAsync(string email, string baseUrl)
    {
        try
        {
            var _user = await _userManager.FindByEmailAsync(email);
            if (_user != null)
            {
                //Forgot Token
                var _forgotToken = _userManager.GeneratePasswordResetTokenAsync(_user);
                // Construct confirmation link
                var confirmationLink = $"{baseUrl.TrimEnd('/')}/authentication/resetpassword?token={Uri.EscapeDataString(_forgotToken.Result)}&email={Uri.EscapeDataString(_user.Email!)}";
                var message = new Message(new string[] { _user.Email! }, "Reset Password Link", confirmationLink!);
                _emailService.SendEmail(message);
                return HelperFunc.MyApiResponse(true, StatusCodes.Status200OK, $"Reset Password, Link Sent to email: {_user!.Email!} Successfully!", new { });
            }
            return HelperFunc.MyApiResponse(false, StatusCodes.Status404NotFound, $"User Not Found!", new { });
        }
        catch (Exception ex)
        {
            return HelperFunc.MyApiResponse(false, StatusCodes.Status500InternalServerError, $"Exception Occured, While Creating User. Inner Exception : {ex.Message}", new { });
        }
    }

    public async Task<ApiResponse> ResetPasswordAsync(string email, string token)
    {
        try
        {
            var _resetPasswordModel = new ResetPasswordViewModel()
            {
                Email = email,
                Token = token
            };
            return HelperFunc.MyApiResponse(true, StatusCodes.Status200OK, "Token Generated Successfully!", new { _resetPasswordModel });
        }
        catch (Exception ex)
        {
            return HelperFunc.MyApiResponse(false, StatusCodes.Status500InternalServerError, $"Exception Occured, While Creating User. Inner Exception : {ex.Message}", new { });
        }
    }

    public async Task<ApiResponse> ResetPasswordAsync(ResetPasswordViewModel resetPasswordViewModel)
    {
        try
        {
            var _request = resetPasswordViewModel;
            var user = await _userManager.FindByEmailAsync(_request.Email!);
            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, _request.Token!, _request.Password!);
                if (result.Succeeded)
                {
                    return HelperFunc.MyApiResponse(true, StatusCodes.Status200OK, "Password Reset Successfully", new { });
                }
                else
                {
                    var _errors = result.Errors.Select(x => x.Description).ToList();
                    return HelperFunc.MyApiResponse(false, StatusCodes.Status400BadRequest, "Error Occured, while Reseting Password", new { }, errors: _errors);
                }
            }
            return HelperFunc.MyApiResponse(false, StatusCodes.Status400BadRequest, "Unable to Reset Password!", new { });
        }
        catch (Exception ex)
        {
            return HelperFunc.MyApiResponse(false, StatusCodes.Status500InternalServerError, $"Exception Occured, While Creating User. Inner Exception : {ex.Message}", new { });
        }
    }

    private async void GenerateEmailConfirmationToken(ApplicationUser user, string baseUrl)
    {
        //Add token to verify the email
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        // Construct confirmation link
        var confirmationLink = $"{baseUrl.TrimEnd('/')}/authentication/confirmemail?token={Uri.EscapeDataString(token)}&email={Uri.EscapeDataString(user.Email!)}";

        var message = new Message(new string[] { user.Email! }, "Confirmation Email", confirmationLink!);
        _emailService.SendEmail(message);
    }

}



// try
// {
//     return HelperFunc.MyApiResponse(true, StatusCodes.Status200OK, "Token Generated Successfully!", new {});
// }
// catch (Exception ex)
// {
//     return HelperFunc.MyApiResponse(false, StatusCodes.Status500InternalServerError, $"Exception Occured, While Creating User. Inner Exception : {ex.Message}", new { });
// }