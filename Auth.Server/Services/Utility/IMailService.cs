using Auth.Server.Models.MailModels;

namespace Auth.Server.Services.Utility
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
