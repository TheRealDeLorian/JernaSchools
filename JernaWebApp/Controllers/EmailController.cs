using JernaClassLib.Data;
using JernaClassLib.Exceptions;
using JernaClassLib.IServices;
using Microsoft.AspNetCore.Mvc;

namespace JernaWebApp.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class EmailController : Controller
{
    IEmailService _emailService;

    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost("sendauth")]
    public async Task SendAuthEmail([FromBody] string email)
    {
        try
        {
            await _emailService.SendAuthEmailAsync(email);
        }
        catch (InvalidEmailException)
        {
            Response.StatusCode = 410;
        }
    }

    [HttpPost("send")]
    public async Task SendEmail([FromBody] EmailInfo emailInfo)
    {
        await _emailService.SendEmailAsync(emailInfo);
    }
}
