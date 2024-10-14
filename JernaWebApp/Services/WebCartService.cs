using JernaClassLib.Data.DatabaseObjects;
using JernaClassLib.IServices;
using JernaWebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace JernaWebApp.Services;

public class WebCartService : ICartService
{
    IDbContextFactory<JernaContext> _factory;
    public WebCartService(IDbContextFactory<JernaContext> contextFactory)
    {
        _factory = contextFactory;
    }
    public async Task<List<CartItem>> GetAllItemsInCartAsync(int userId)
    {
        var context = await _factory.CreateDbContextAsync();
        var usersCart = await GetOrCreateCartAsync(userId);

        return await context.CartItems.Where(ci => ci.CartId == usersCart.Id)
            .Include(ci => ci.Item)
            .Select(ci => new CartItem
            {
                Id = ci.Id,
                CartId = ci.CartId,
                ItemId = ci.ItemId,
                Quantity = ci.Quantity,
                ContactInfo = ci.ContactInfo,
                Item = new Item
                {
                    Id = ci.Item.Id,
                    ItemName = ci.Item.ItemName,
                    Price = ci.Item.Price,
                    Description = ci.Item.Description,
                    Image = ci.Item.Image,
                    Isdisplayed = ci.Item.Isdisplayed,
                    Mediafile = null,
                    IsDigital = ci.Item.IsDigital,
                    IsPhysical = ci.Item.IsPhysical,
                    PeriodLengthId = ci.Item.PeriodLengthId
                }
            })
            .ToListAsync();
    }

    public async Task AddCartItemAsync(int userId, int itemId, int amount = 1)
    {
        var context = await _factory.CreateDbContextAsync();
        Cart usersCart = await GetOrCreateCartAsync(userId);

        if (usersCart.CartItems.Any(ci => ci.ItemId == itemId))
        {
            var cartItem = usersCart.CartItems.Single(ci => ci.ItemId == itemId);
            cartItem.Quantity++;
            context.CartItems.Update(cartItem);
            await context.SaveChangesAsync();
        }
        else
        {
            context.CartItems.Add(new()
            {
                CartId = usersCart.Id,
                ItemId = itemId,
                Quantity = amount,
            });
            await context.SaveChangesAsync();
        }

    }

    public async Task AddCartItemAsync(int userId, int itemId, string? contactInfo)
    {
        var context = await _factory.CreateDbContextAsync();
        Cart usersCart = await GetOrCreateCartAsync(userId);

        if (usersCart.CartItems.Any(ci => ci.ItemId == itemId))
        {
            var cartItem = usersCart.CartItems.Single(ci => ci.ItemId == itemId);
            cartItem.Quantity++;
            context.CartItems.Update(cartItem);
            await context.SaveChangesAsync();
        }
        else
        {
            context.CartItems.Add(new()
            {
                CartId = usersCart.Id,
                ItemId = itemId,
                Quantity = 1,
                ContactInfo = contactInfo
            });
            await context.SaveChangesAsync();
        }

    }

    public async Task EditCartItemQtyAsync(CartItem cartItem)
    {
        var context = await _factory.CreateDbContextAsync();

        if (cartItem.Quantity == 0)
            await context.CartItems.Where(ci => ci.Id == cartItem.Id).ExecuteDeleteAsync();
        else
            context.CartItems.Update(cartItem);
        await context.SaveChangesAsync();
    }

    public async Task EmptyCartAsync(int userId)
    {
        var context = await _factory.CreateDbContextAsync();

        var usersCart = await GetOrCreateCartAsync(userId);
        var cartItems = await context.CartItems.Where(ci => ci.CartId == usersCart.Id).ToListAsync();

        if (cartItems.Any())
        {
            context.CartItems.RemoveRange(cartItems);
            await context.SaveChangesAsync();
        }
    }

    async Task<Cart> GetOrCreateCartAsync(int userId)
    {
        var context = await _factory.CreateDbContextAsync();

        return await context.Carts.Where(c => c.UserId == userId)
            .Include(c => c.CartItems)
            .SingleOrDefaultAsync() ?? await CreateCartAsync(userId);
    }

    async Task<Cart> CreateCartAsync(int userId)
    {
        var context = await _factory.CreateDbContextAsync();
        Cart c = new() { UserId = userId };

        await context.Carts.AddAsync(c);
        await context.SaveChangesAsync();
        return c;
    }
}
