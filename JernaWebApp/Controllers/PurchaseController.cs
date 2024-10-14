using JernaClassLib.Data.DatabaseObjects;
using JernaClassLib.IServices;
using Microsoft.AspNetCore.Mvc;

namespace JernaWebApp.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class PurcahseController : Controller
{
    IPurchaseService _purchaseService;

    public PurcahseController(IPurchaseService purServ)
    {
        _purchaseService = purServ;
    }
    [HttpPost("previous")]
    public async Task<List<Purchase>> GetPastPurchasesAsync([FromBody] int userId)
    {
        return await _purchaseService.GetPastPurchasesAsync(userId);
    }
    [HttpPost("cart")]
    public async Task<bool> PurchaseCartAsync([FromBody] (int, double) userId_taxPercent)
    {
        return await _purchaseService.PurchaseCartAsync(userId_taxPercent.Item1, userId_taxPercent.Item2);
    }
}
