using JernaClassLib.Data.DatabaseObjects;
using JernaClassLib.IServices;
using Microsoft.AspNetCore.Mvc;

namespace JernaWebApp.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ItemController : Controller
{
    IItemService _itemService;

    public ItemController(IItemService itemService)
    {
        _itemService = itemService;
    }

    [HttpGet("all")]
    public async Task<List<Item>> GetAllItemsAsync()
    {
        var l = await _itemService.GetAllItemsAsync();
        return l;
    }
}
