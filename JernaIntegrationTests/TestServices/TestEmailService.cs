using JernaClassLib;
using JernaClassLib.Data;
using JernaClassLib.IServices;
using JernaWebApp.IWebServices;
using JernaClassLib.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JernaIntegrationTests.TestServices;

public class TestEmailService : IEmailService
{
    readonly IWebAuthUtilityService _webAuthUtilityService;
    public TestEmailService(IWebAuthUtilityService webAuthUtilityService)
    {
        _webAuthUtilityService = webAuthUtilityService;
    }

    public async Task SendAuthEmailAsync(string email)
    {
        if (!Constants.IsValidEmail(email))
        {
            throw new InvalidEmailException();
        }

        var user = await _webAuthUtilityService.UserOrNullAsync(email) ?? await _webAuthUtilityService.CreateUserAsync(email);

        string subject = "Authenticate your JernaSchools account";
        var tempCode = await _webAuthUtilityService.CreateTempCodeAsync(user.Id);
        string message = $"Hello! here is your temporary code: {tempCode.Code} Please insert this into your login.";

        EmailInfo emailInfo = new()
        {
            Email = email,
            Subject = subject,
            HTMLBody = message
        };

        await SendEmailAsync(emailInfo);
    }

    public Task SendEmailAsync(EmailInfo emailInfo)
    {
        return Task.CompletedTask;
    }
}
