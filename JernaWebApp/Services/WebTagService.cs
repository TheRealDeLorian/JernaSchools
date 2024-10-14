using JernaClassLib.Data.DatabaseObjects;
using JernaClassLib.IServices;
using JernaWebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace JernaWebApp.Services;

public class WebTagService : ITagService
{
    IDbContextFactory<JernaContext> _factory;
    public WebTagService(IDbContextFactory<JernaContext> contextFactory)
    {
        _factory = contextFactory;
    }

    public async Task<List<Tag>> GetAllTagsAsync()
    {
        var context = await _factory.CreateDbContextAsync();
        return await context.Tags.ToListAsync();
    }
}
