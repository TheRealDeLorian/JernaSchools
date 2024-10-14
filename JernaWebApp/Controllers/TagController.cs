using JernaClassLib.Data.DatabaseObjects;
using JernaClassLib.IServices;
using Microsoft.AspNetCore.Mvc;

namespace JernaWebApp.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TagController
{
    ITagService _tagService;

    public TagController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet("all")]
    public async Task<List<Tag>> GetAllTagsAsync()
    {
        return await _tagService.GetAllTagsAsync();
    }
}
