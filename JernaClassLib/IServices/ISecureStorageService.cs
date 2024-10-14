namespace JernaClassLib.IServices;

public interface ISecureStorageService // Not secure for Jerna Web App
{
    public Task<bool> StoreKeyValueAsync(string key, string value);
    public Task<string?> GetValueFromKeyAsync(string key);
    public Task<bool> DeleteKeyValueAsync(string key);

}
