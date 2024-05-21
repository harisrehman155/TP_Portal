using TP_Portal.Model.Email;

namespace TP_Portal.Services.Email;

public interface IEmailService
{
    void SendEmail(Message message);
}