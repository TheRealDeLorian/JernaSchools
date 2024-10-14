using JernaClassLib.Data.DatabaseObjects;
using JernaClassLib.IServices;
using System.Net.Http.Json;

namespace JernaMaui.Services;

public class MauiItemService : IItemService
{
    HttpClient _httpClient;
    public MauiItemService(HttpClient hc)
    {
        _httpClient = hc;
    }
    public async Task<List<Item>> GetAllItemsAsync()
    {
        var l = await _httpClient.GetFromJsonAsync<List<Item>>("item/all") ?? throw new NullReferenceException("Could not get list of items");
        return l;
    }
}
