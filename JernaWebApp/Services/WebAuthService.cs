using JernaClassLib.Data;
using JernaClassLib.Data.DatabaseObjects;
using JernaClassLib.Exceptions;
using JernaClassLib.IServices;
using JernaWebApp.Data;
using JernaWebApp.IWebServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace JernaWebApp.Services;

public class WebAuthService : IAuthService
{
    IWebAuthUtilityService _webAuthUtilityService;
    IDbContextFactory<JernaContext> _dbContextFactory;
    public WebAuthService(IWebAuthUtilityService waus, IDbContextFactory<JernaContext> factory)
    {
        _webAuthUtilityService = waus;
        _dbContextFactory = factory;
    }

    public async Task<string> VerifyTempReturnAuthAsync(string code)
    {
        User user = ((await _webAuthUtilityService.GetAllTempCodesAsync()).Where(tc => tc.Code == code).FirstOrDefault()?.User) ?? throw new NullReferenceException("User was null");
        await _webAuthUtilityService.DeleteTempCodeAsync(code);
        return await _webAuthUtilityService.CreateAuthCodeAsync(user.Id);
    }

    public async Task<bool> IsValidAuthCodeAsync(string code)
    {
        var authCodes = await _webAuthUtilityService.GetAllAuthCodesAsync();
        return authCodes.Where(ac => ac.Code == code).Any();
    }

    public async Task<User?> GetUserAsync(string authCode)
    {
        var context = _dbContextFactory.CreateDbContext();
        return await context.Users
            .Include(u => u.UserAuthCodes)
            .ThenInclude(ua => ua.AuthCode)
            .FirstOrDefaultAsync(u => u.UserAuthCodes.Any(ua => ua.AuthCode != null && ua.AuthCode.Code == authCode));
    }

    public async Task<UserDTO> GenerateRandomUserAsync()
    {
        var u = await _webAuthUtilityService.CreateRandomUserAsync();
        return new UserDTO()
        {
            User = await _webAuthUtilityService.CreateRandomUserAsync(),
            AuthCode = await _webAuthUtilityService.CreateAuthCodeAsync(u.Id)
        };
    }
}
