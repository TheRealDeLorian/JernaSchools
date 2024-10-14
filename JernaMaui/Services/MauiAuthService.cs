using JernaClassLib.Data;
using JernaClassLib.Data.DatabaseObjects;
using JernaClassLib.Exceptions;
using JernaClassLib.IServices;
using System.Net.Http.Json;

namespace JernaMaui.Services;

internal class MauiAuthService : IAuthService
{
    HttpClient _httpClient;
    public MauiAuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> IsValidAuthCodeAsync(string code)
    {
        var response = await _httpClient.PostAsJsonAsync("authentication/auth_code", code);

        if (!response.IsSuccessStatusCode)
            throw new FailedAPICall("The users code was not valid");

        return await response.Content.ReadFromJsonAsync<bool>();
    }

    public async Task<string> VerifyTempReturnAuthAsync(string code)
    {
        var response = await _httpClient.PostAsJsonAsync("authentication/temp_code", code);

        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            throw new FailedAPICall("The temp code was invalid");

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<User?> GetUserAsync(string authCode)
    {
        var response = await _httpClient.PostAsJsonAsync("authentication/get_user", authCode);

        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            throw new InvalidAuthCodeException();

        return await response.Content.ReadFromJsonAsync<User>() ?? throw new NullReferenceException("Could not User from API call");
    }

    public async Task<UserDTO> GenerateRandomUserAsync()
    {
        return await _httpClient.GetFromJsonAsync<UserDTO>("authentication/generate_random_user") ?? throw new ImpossibleException();
    }
}
