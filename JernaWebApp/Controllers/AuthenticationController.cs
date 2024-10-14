using JernaClassLib.Data;
using JernaClassLib.Data.DatabaseObjects;
using JernaClassLib.IServices;
using Microsoft.AspNetCore.Mvc;

namespace JernaWebApp.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthenticationController : Controller
{
    IAuthService _authService;

    public AuthenticationController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("temp_code")]
    public async Task<string> ValidateTempCodeAsync([FromBody] string tempCode)
    {
        var result = "";

        try
        {
            result = await _authService.VerifyTempReturnAuthAsync(tempCode);
        }
        catch
        {
            Response.StatusCode = 401;
        }

        return result;
    }

    [HttpPost("auth_code")]
    public async Task<bool> ValidateAuthCodeAsync([FromBody] string authCode)
    {
        return await _authService.IsValidAuthCodeAsync(authCode);
    }

    [HttpPost("get_user")]
    public async Task<User?> GetUserAsync([FromBody] string authCode)
    {
        var result = await _authService.GetUserAsync(authCode);
        return result;
    }

    [HttpGet("generate_random_user")]
    public async Task<UserDTO> GenerateRndUserAsync()
    {
        var us = await _authService.GenerateRandomUserAsync();
        return us;
    }
}
