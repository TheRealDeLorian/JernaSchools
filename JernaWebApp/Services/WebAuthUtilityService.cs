using JernaClassLib.Data.DatabaseObjects;
using JernaWebApp.Data;
using JernaWebApp.IWebServices;
using Microsoft.EntityFrameworkCore;

namespace JernaWebApp.Services;

public class WebAuthUtilityService : IWebAuthUtilityService
{
    private IDbContextFactory<JernaContext> _factory;
    public WebAuthUtilityService(IDbContextFactory<JernaContext> contextFactory)
    {
        _factory = contextFactory;
    }
    public async Task<User> CreateUserAsync(string email)
    {
        var context = await _factory.CreateDbContextAsync();

        User user = new()
        {
            Email = email,
            Username = email.Split('@')[0]
        };
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return context.Users.Where(u => u.Email == email).Single();
    }

    public async Task<List<TempCode>> GetAllTempCodesAsync()
    {
        DateTime thresholdDateTime = DateTime.Now.Subtract(new TimeSpan(0, 30, 0));
        var context = await _factory.CreateDbContextAsync();
        return await context.TempCodes
            .Where(tc => tc.Createdate > thresholdDateTime)
            .Include(tc => tc.User)
            .ToListAsync();
    }

    public async Task<List<AuthCode>> GetAllAuthCodesAsync()
    {
        var context = await _factory.CreateDbContextAsync();
        return await context.AuthCodes
            .Include(ac => ac.UserAuthCodes)
            .ThenInclude(uac => uac.User)
            .ToListAsync();
    }

    public async Task<User?> UserOrNullAsync(string email)
    {
        var context = await _factory.CreateDbContextAsync();
        return (await context.Users.Where(u => u.Email == email).ToListAsync()).FirstOrDefault();
    }

    public async Task<TempCode> CreateTempCodeAsync(int userId)
    {
        var context = await _factory.CreateDbContextAsync();
        char[] hash = new char[16];

        for (int i = 0; i < 16; i++)
            hash[i] = (char)Random.Shared.Next(65, 91);

        IEnumerable<TempCode> tempCodes = await context.TempCodes.ToListAsync();

        if (tempCodes.Where(t => t.Code == hash.ToString()).Count() > 0)
            return await CreateTempCodeAsync(userId);

        TempCode tempCode = new()
        {
            Userid = userId,
            Code = new string(hash),
            Createdate = DateTime.Now,
        };

        await context.TempCodes.AddAsync(tempCode);
        await context.SaveChangesAsync();

        return tempCode;
    }

    public async Task DeleteTempCodeAsync(string code)
    {
        var context = await _factory.CreateDbContextAsync();
        var deletedCode = await context.TempCodes.Where(tc => tc.Code == code).SingleAsync();
        context.Remove(deletedCode);
        await context.SaveChangesAsync();
    }

    public async Task<string> CreateAuthCodeAsync(int userId)
    {
        var context = await _factory.CreateDbContextAsync();
        var authCode = await CreateAuthCodeAsync();
        await context.AuthCodes.AddAsync(authCode);
        await context.UserAuthCodes.AddAsync(new UserAuthCode() { AuthCode = authCode, UserId = userId });
        await context.SaveChangesAsync();
        return authCode.Code;
    }

    async Task<AuthCode> CreateAuthCodeAsync()
    {
        var context = await _factory.CreateDbContextAsync();
        var r = new Random();
        char[] hash = new char[16];

        for (int i = 0; i < 16; i++)
            hash[i] = (char)r.Next(33, 127);

        IEnumerable<AuthCode> authCodes = await context.AuthCodes.ToListAsync();

        if (authCodes.Where(t => t.Code == hash.ToString()).Count() > 0)
            return await CreateAuthCodeAsync();

        return new AuthCode() { Code = new string(hash) };
    }

    public async Task<User> CreateRandomUserAsync()
    {
        var context = await _factory.CreateDbContextAsync();
        string email = (await CreateAuthCodeAsync()).Code + "@RandomEmail.com";

        User user = new()
        {
            Email = email,
            Username = "Random User"
        };

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return context.Users.Where(u => u.Email == email).Single();
    }
}
