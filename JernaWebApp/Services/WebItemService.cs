using JernaClassLib.Data.DatabaseObjects;
using JernaClassLib.IServices;
using JernaWebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace JernaWebApp.Services;

public class WebItemService : IItemService
{
    private readonly IDbContextFactory<JernaContext> _factory;

    public WebItemService(IDbContextFactory<JernaContext> contextFactory)
    {
        _factory = contextFactory;
    }

    public async Task<List<Item>> GetAllItemsAsync()
    {
        var context = await _factory.CreateDbContextAsync();

        return await context.Items
            .Include(i => i.TagItems)
            .ThenInclude(ti => ti.Tag)
            .Select(item => new Item
            {
                Id = item.Id,
                ItemName = item.ItemName,
                Price = item.Price,
                Description = item.Description,
                Image = item.Image,
                Isdisplayed = item.Isdisplayed,
                Mediafile = null,
                IsDigital = item.IsDigital,
                IsPhysical = item.IsPhysical,
                PeriodLengthId = item.PeriodLengthId,
                TagItems = item.TagItems
            })
            .ToListAsync();
    }

    public async Task<Item> GetItemByIdAsync(int id)
    {
        var context = await _factory.CreateDbContextAsync();

        return await context.Items.FirstAsync(item => item.Id == id);
    }

    public async Task CreateItemAsync(Item item)
    {
        var context = await _factory.CreateDbContextAsync();

        context.Items.Add(item);
        await context.SaveChangesAsync();
    }

    public async Task UpdateItemAsync(Item item)
    {
        var context = await _factory.CreateDbContextAsync();

        context.Update(item);
        await context.SaveChangesAsync();
    }

    public async Task DeleteItemAsync(int id)
    {
        var context = await _factory.CreateDbContextAsync();

        var item = await context.Items.FindAsync(id);
        if (item != null)
        {
            context.Items.Remove(item);
            try
            {
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
