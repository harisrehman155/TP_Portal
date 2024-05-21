using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TP_Portal.Context;

namespace TP_Portal.Helper;

public class HelperFunc
{
    private readonly IConfiguration _configuration;
    public HelperFunc(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public static JwtSecurityToken GetToken(List<Claim> authClaims, IConfiguration _configuration)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"]!,              //This Application Url. localhost:port no
            audience: _configuration["JWT:ValidAudience"]!,         //FrontEnd Application Url. Eg.React,Angular etc.
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
        return token;

    }

    public static ApiResponse MyApiResponse(
        bool isSuccess, int status, string msg, object response, List<string> errors = null!)
    {
        ApiResponse apiResponse = new ApiResponse()
        {
            IsSuccess = isSuccess,
            Status = status,
            Message = msg,
            Response = response,
            Errors = errors
        };
        return apiResponse;
    }



}