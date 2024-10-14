using JernaClassLib.Data.DatabaseObjects;
using JernaClassLib.IServices;
using System.Net.Http.Json;

namespace JernaMaui.Services;

public class MauiPurchaseService : IPurchaseService
{
    HttpClient _httpClient;
    public MauiPurchaseService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Purchase>> GetPastPurchasesAsync(int userId)
    {
        var result = await _httpClient.PostAsJsonAsync("purchase/previous", userId);
        return await result.Content.ReadFromJsonAsync<List<Purchase>>() ?? throw new NullReferenceException("There was no list of purchases");
    }

    public async Task<bool> PurchaseCartAsync(int userId, double taxPercent = 0.047)
    {
        var result = await _httpClient.PostAsJsonAsync("purchase/cart", (userId, taxPercent));
        return await result.Content.ReadFromJsonAsync<bool>();
    }
}
