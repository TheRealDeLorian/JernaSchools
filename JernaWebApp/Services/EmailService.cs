using JernaClassLib;
using JernaClassLib.Data;
using JernaClassLib.Exceptions;
using JernaClassLib.Forms;
using JernaClassLib.IServices;
using JernaWebApp.IWebServices;
using MimeKit;

namespace JernaWebApp.Services;

public class EmailService : IEmailService
{
    readonly IConfiguration _configuration;
    readonly IWebAuthUtilityService _webAuthUtilityService;
    public EmailService(IConfiguration config, IWebAuthUtilityService webAuthUtilityService)
    {
        _webAuthUtilityService = webAuthUtilityService;
        _configuration = config;
    }

    public async Task SendAuthEmailAsync(string email)
    {
        if (!Constants.IsValidEmail(email))
            throw new InvalidEmailException();

        var user = await _webAuthUtilityService.UserOrNullAsync(email) ?? await _webAuthUtilityService.CreateUserAsync(email);

        string subject = "Authenticate your JernaSchools account";
        var tempCode = await _webAuthUtilityService.CreateTempCodeAsync(user.Id);

        string message = $@"
                    <!DOCTYPE html>
                    <html lang=""en"">
                    <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <style>
                    .container {{
                        display: flex;
                        align-items: center;
                    }}
                    </style>
                    </head>
                    <body>
                    <p>Thank you for signing up with JernaSchools! Please copy this code and paste it into our login page!</p>
                    <br />
                    <div class=""container"">
                    <h3>Code: </h3>
                    <input type=""text"" value=""{tempCode.Code}"" id=""textToCopy"" />
                    </div>
                    </body>
                    </html>
            ";

        EmailInfo emailInfo = new()
        {
            Email = email,
            Subject = subject,
            HTMLBody = message
        };

        await SendEmailAsync(emailInfo);
    }

    public async Task SendEmailAsync(EmailInfo emailInfo)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Jerna Schools", Constants.EmailAddress));
        message.To.Add(new MailboxAddress("", emailInfo.Email));
        message.Subject = emailInfo.Subject;

        var builder = new BodyBuilder
        {
            HtmlBody = emailInfo.HTMLBody
        };
        message.Body = builder.ToMessageBody();

        using (var client = new MailKit.Net.Smtp.SmtpClient())
        {
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate(Constants.EmailAddress, $"{_configuration[Constants.ConfigKeyForEmailPass]}");
            client.Send(message);
            client.Disconnect(true);
        }

        await Task.CompletedTask;
    }
}
