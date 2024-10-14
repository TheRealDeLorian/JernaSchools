using JernaClassLib.Data;

namespace JernaClassLib.IServices;

public interface IAuthService
{
    Task<string> VerifyTempReturnAuthAsync(string code);
    Task<bool> IsValidAuthCodeAsync(string code);
    Task<User?> GetUserAsync(string authCode);
    Task<UserDTO> GenerateRandomUserAsync();
}
