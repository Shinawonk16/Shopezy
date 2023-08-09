using Application.Dtos;

namespace Persistence.Email;

public interface IEmailService
{
    public void SendEMailAsync(MailRequest mailRequest);
}
