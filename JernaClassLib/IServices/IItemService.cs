namespace JernaClassLib.IServices;

public interface IItemService
{
    //CRUD
    Task<List<Item>> GetAllItemsAsync();
    //Task<List<Item>> AddItemAsync(Item item);
    //Task<List<Item>> EditItemAsync(Item item);
    //Task<List<Item>> DeleteItemAsync(int itemId);
}
