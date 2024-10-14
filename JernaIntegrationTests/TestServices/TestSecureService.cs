using JernaClassLib.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JernaIntegrationTests.TestServices;

public class TestSecureService : ISecureStorageService
{
    private Dictionary<string, string> _dictionary = new();
    public Task<bool> DeleteKeyValueAsync(string key)
    {
        _dictionary.Remove(key);
        return Task.FromResult(true);
    }

    public Task<string?> GetValueFromKeyAsync(string key)
    {
        return Task.FromResult(_dictionary.GetValueOrDefault(key));
    }

    public Task<bool> StoreKeyValueAsync(string key, string value)
    {
        _dictionary.Add(key, value);
        return Task.FromResult(true);
    }
}
