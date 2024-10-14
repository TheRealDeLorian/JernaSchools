using JernaClassLib.Data;
using JernaClassLib.Data.DatabaseObjects;
using JernaClassLib.IServices;
using System.Net.Http.Json;

namespace JernaMaui.Services;

public class MauiCartService : ICartService
{
    HttpClient _httpClient;
    public MauiCartService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task AddCartItemAsync(int userId, int itemId, int amount = 1)
    {
        await _httpClient.PostAsJsonAsync("cart/add-item", new AddThisToCart() { UserId = userId, ItemId = itemId, Amount = amount });
    }

    public async Task AddCartItemAsync(int userId, int itemId, string? contactInfo)
    {
        await _httpClient.PostAsJsonAsync("cart/add-coaching-item", new AddThisToCart() { UserId = userId, ItemId = itemId, Amount = 1, ContactInfo = contactInfo });
    }

    public async Task EditCartItemQtyAsync(CartItem cartItem)
    {
        await _httpClient.PostAsJsonAsync("cart/edit", cartItem);
    }

    public async Task EmptyCartAsync(int userId)
    {
        await _httpClient.PostAsJsonAsync("cart/empty", userId);
    }

    public async Task<List<CartItem>> GetAllItemsInCartAsync(int userId)
    {
        var result = await _httpClient.PostAsJsonAsync("cart/all", userId);
        return await result.Content.ReadFromJsonAsync<List<CartItem>>() ?? throw new NullReferenceException("Could not get all items in cart");
    }
}
