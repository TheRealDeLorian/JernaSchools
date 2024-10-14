using JernaClassLib.Data;
using JernaClassLib.Data.DatabaseObjects;
using JernaClassLib.IServices;
using JernaWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace JernaWebApp.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CartController
{
    ICartService _cartService;
    public CartController(ICartService cartServ)
    {
        _cartService = cartServ;
    }

    [HttpPost("all")]
    public async Task<List<CartItem>> GetAllItemsInCartAsync([FromBody] int userId)
    {
        return await _cartService.GetAllItemsInCartAsync(userId);
    }

    [HttpPost("add-item")]
    public async Task AddCartItemAsync([FromBody] AddThisToCart userId_itemId_amount)
    {
        await _cartService.AddCartItemAsync(userId_itemId_amount.UserId, userId_itemId_amount.ItemId, userId_itemId_amount.Amount);
    }

    [HttpPost("add-coaching-item")]
    public async Task AddCoachingItemAsync([FromBody] AddThisToCart userId_itemId_contactInfo)
    {
        await _cartService.AddCartItemAsync(userId_itemId_contactInfo.UserId, userId_itemId_contactInfo.ItemId, userId_itemId_contactInfo.ContactInfo);
    }

    [HttpPost("edit")]
    public async Task EditCartItemQtyAsync([FromBody] CartItem cartItem)
    {
        await _cartService.EditCartItemQtyAsync(cartItem);
    }

    [HttpPost("empty")]
    public async Task EmptyCartAsync([FromBody] int userId)
    {
        await _cartService.EmptyCartAsync(userId);
    }
}
