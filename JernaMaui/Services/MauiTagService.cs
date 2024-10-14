using JernaClassLib.Data.DatabaseObjects;
using JernaClassLib.IServices;
using System.Net.Http.Json;

namespace JernaMaui.Services;

public class MauiTagService : ITagService
{
    HttpClient _httpClient;

    public MauiTagService(HttpClient hc)
    {
        _httpClient = hc;
    }
    public async Task<List<Tag>> GetAllTagsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Tag>>("tag/all") ?? throw new NullReferenceException("Could not get list of tags");
    }
}
