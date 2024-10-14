using JernaClassLib.Data;

namespace JernaClassLib.IServices;

public interface IEmailService
{
    Task SendAuthEmailAsync(string email);
    Task SendEmailAsync(EmailInfo emailInfo);
}
