using JernaClassLib.Data.DatabaseObjects;

namespace JernaWebApp.IWebServices;

public interface IWebAuthUtilityService
{
    Task<User?> UserOrNullAsync(string email);
    Task<User> CreateUserAsync(string email);
    Task<List<TempCode>> GetAllTempCodesAsync();
    Task<List<AuthCode>> GetAllAuthCodesAsync();
    Task<TempCode> CreateTempCodeAsync(int userId);
    Task DeleteTempCodeAsync(string code);
    Task<string> CreateAuthCodeAsync(int userId);
    Task<User> CreateRandomUserAsync();
}
