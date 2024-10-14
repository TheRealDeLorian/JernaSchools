namespace JernaClassLib.IServices;

public interface ICartService
{
    Task<List<CartItem>> GetAllItemsInCartAsync(int userId);
    Task EditCartItemQtyAsync(CartItem cartItem);
    Task EmptyCartAsync(int userId);
    Task AddCartItemAsync(int userId, int itemId, int amount = 1);
    Task AddCartItemAsync(int userId, int itemId, string? contactInfo);
}
