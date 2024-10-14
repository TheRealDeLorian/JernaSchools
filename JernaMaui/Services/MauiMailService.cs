using JernaClassLib.Data;
using JernaClassLib.Forms;
using JernaClassLib.IServices;
using System.Net.Http.Json;

namespace JernaMaui.Services;

public class MauiMailService : IEmailService
{
    HttpClient _client;
    public MauiMailService(HttpClient client)
    {
        _client = client;
    }

    public async Task SendAuthEmailAsync(string email)
    {
        var result = await _client.PostAsJsonAsync("email/sendauth", email);
        if (result.StatusCode != System.Net.HttpStatusCode.OK)
            throw new Exception("Something went wrong on our end.");
    }

    public async Task SendEmailAsync(EmailInfo emailInfo)
    {
        var result = await _client.PostAsJsonAsync("email/send", emailInfo);
        if (result.StatusCode != System.Net.HttpStatusCode.OK)
            throw new Exception("Something went wrong on our end.");
    }
}
