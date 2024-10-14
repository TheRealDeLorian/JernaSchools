using JernaClassLib.IServices;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace JernaWebApp.Services;

public class WebSecureStorageService : ISecureStorageService
{
    private ProtectedLocalStorage _protectedLocalStorage;
    public WebSecureStorageService(ProtectedLocalStorage protectedLocalStorage)
    {
        _protectedLocalStorage = protectedLocalStorage;
    }
    public async Task<bool> DeleteKeyValueAsync(string key)
    {
        await _protectedLocalStorage.DeleteAsync(key);
        return true;
    }

    public async Task<string?> GetValueFromKeyAsync(string key)
    {
        return (await _protectedLocalStorage.GetAsync<string>(key)).Value;
    }

    public async Task<bool> StoreKeyValueAsync(string key, string value)
    {
        await _protectedLocalStorage.SetAsync(key, value);
        return true;
    }
}
