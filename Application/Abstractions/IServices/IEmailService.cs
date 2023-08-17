using Application.Dtos;

namespace Application.Abstractions.IServices;


public interface IEmailService
{
    public void SendEMailAsync(MailRequest mailRequest);
}
