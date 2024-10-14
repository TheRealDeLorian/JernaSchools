namespace JernaClassLib.IServices;

public interface IPurchaseService
{
    Task<bool> PurchaseCartAsync(int userId, double taxPercent = 0.047);
    Task<List<Purchase>> GetPastPurchasesAsync(int userId);
}
