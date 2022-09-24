using OzonTestMailSender.Core.Models;

namespace OzonTestMailSender.Core.BL;

public interface IEmailService
{
    Task Send(EmailMessage message, CancellationToken token);
    Task<IEnumerable<SentMessageResult>> GetMessageHistory();
}