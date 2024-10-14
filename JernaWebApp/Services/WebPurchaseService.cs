using JernaClassLib.Data.DatabaseObjects;
using JernaClassLib.Exceptions;
using JernaClassLib.IServices;
using JernaWebApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace JernaWebApp.Services;

public class WebPurchaseService : IPurchaseService
{
    IDbContextFactory<JernaContext> _factory;
    ICartService _cartService;
    public WebPurchaseService(IDbContextFactory<JernaContext> contextFactory, ICartService cs)
    {
        _factory = contextFactory;
        _cartService = cs;
    }
    public async Task<List<Purchase>> GetPastPurchasesAsync(int userId)
    {
        var context = await _factory.CreateDbContextAsync();

        return await context.Purchases.Where(p => p.UserId == userId)
            .Include(p => p.PurchaseItems)
            .ThenInclude(pi => pi.Item)
            .ThenInclude(i => i!.TagItems)
            .ThenInclude(ti => ti.Tag)
            .ToListAsync();
    }

    public async Task<bool> PurchaseCartAsync(int userId, double taxPercent = 0.047)
    {
        var context = await _factory.CreateDbContextAsync();
        List<CartItem> cartItems = await _cartService.GetAllItemsInCartAsync(userId);

        Purchase newPurchase = new()
        {
            UserId = userId,
            PurchaseDate = DateTime.Now,
            Taxpercent = taxPercent           //TaxRate
        };

        context.Purchases.Add(newPurchase);
        await context.SaveChangesAsync();

        Purchase? dbPuchase = await context.Purchases.Where(p => p.UserId == userId)
            .OrderBy(p => Math.Abs((p.PurchaseDate - DateTime.Now).TotalDays))
            .FirstOrDefaultAsync() ?? throw new ImpossibleException();


        context.PurchaseItems.AddRange(cartItems.Select(item => new PurchaseItem
        {
            Quantity = item.Quantity,
            PurchaseId = dbPuchase.Id,
            ItemId = item.ItemId,
        }).ToList());

        await context.SaveChangesAsync();


        decimal itemsPrice = cartItems.Sum(ci => ci.Item.Price * ci.Quantity);

        // Where to put any discounts

        Transaction newTransaction = new()
        {
            PurchasePrice = itemsPrice * (decimal)taxPercent,
            PurchaseId = dbPuchase.Id
        };

        await _cartService.EmptyCartAsync(userId);
        context.Transactions.Add(newTransaction);
        await context.SaveChangesAsync();

        return true;
    }
}
