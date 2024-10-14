namespace JernaClassLib.IServices;

public interface ITagService
{
    Task<List<Tag>> GetAllTagsAsync();
}
