using JernaClassLib.IServices;

namespace JernaMaui.Services
{
    public class MauiSecureStorageService : ISecureStorageService
    {
        ISecureStorage _secureStorage;
        public MauiSecureStorageService(ISecureStorage ss)
        {
            _secureStorage = ss;
        }
        public Task<bool> DeleteKeyValueAsync(string key)
        {
            return Task.FromResult(_secureStorage.Remove(key));
        }

        public async Task<string?> GetValueFromKeyAsync(string key)
        {
            var j = await _secureStorage.GetAsync(key);
            return j;
        }

        public async Task<bool> StoreKeyValueAsync(string key, string value)
        {
            try
            {
                await _secureStorage.SetAsync(key, value);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
