using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using TP_Portal.Repositories;
using TP_Portal.ViewModel;

namespace TP_Portal.Controllers;

[AllowAnonymous]
[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationRepository _authencationRepository;
    private readonly IActionContextAccessor _actionContextAccessor;
    private readonly IUrlHelperFactory _urlHelperFactory;
    public AuthenticationController(IAuthenticationRepository authencationRepository, IActionContextAccessor actionContextAccessor, IUrlHelperFactory urlHelperFactory)
    {
        _authencationRepository = authencationRepository;
        _actionContextAccessor = actionContextAccessor;
        _urlHelperFactory = urlHelperFactory;
    }

    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse>> Register(RegisterViewModel registerViewModel)
    {
        var baseUrl = GetBaseUrl();
        ApiResponse _response = await _authencationRepository.RegisterUserAsync(registerViewModel, baseUrl);
        return Ok(_response);
    }
    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse>> Login(LoginViewModel loginViewModel)
    {
        ApiResponse _response = await _authencationRepository.LoginUserAsync(loginViewModel);
        return Ok(_response);
    }
    [HttpGet("logout")]
    public async Task<ActionResult> Logout()
    {
        ApiResponse _response = await _authencationRepository.LogoutUserAsync();
        return Ok(_response);
    }

    [HttpGet("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail(string token, string email)
    {
        var baseUrl = GetBaseUrl();
        ApiResponse _response = await _authencationRepository.ConfirmEmailAsync(token, email, baseUrl);
        return Ok(_response);
    }

    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword([Required] string email)
    {
        var baseUrl = GetBaseUrl();
        ApiResponse _response = await _authencationRepository.ForgotPasswordAsync(email, baseUrl);
        return Ok(_response);
    }

    [HttpGet("ResetPassword")]
    public async Task<IActionResult> ResetPassword(string email, string token)
    {
        ApiResponse _response = await _authencationRepository.ResetPasswordAsync(email, token);
        return Ok(_response);
    }

    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
    {
        ApiResponse _response = await _authencationRepository.ResetPasswordAsync(resetPasswordViewModel);
        return Ok(_response);
    }

    private string GetBaseUrl()
    {
        var actionContext = _actionContextAccessor.ActionContext;
        var urlHelper = _urlHelperFactory.GetUrlHelper(actionContext!);
        var scheme = actionContext!.HttpContext.Request.Scheme;
        return $"{scheme}://{actionContext.HttpContext.Request.Host}";
    }
}
