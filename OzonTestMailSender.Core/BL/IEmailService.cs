using OzonTestMailSender.Core.Models;

namespace OzonTestMailSender.Core.BL;

public interface IEmailService
{
    Task Send(EmailMessage message);
    Task<IEnumerable<SentMessageResult>> GetMessageHistory();
}