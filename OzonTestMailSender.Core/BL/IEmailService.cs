using FluentResults;
using OzonTestMailSender.Core.Models;

namespace OzonTestMailSender.Core.BL;

public interface IEmailService
{
    Task Send(EmailMessage message, CancellationToken token);
    Task<Result<IEnumerable<SentMessageResult>>> GetMessageHistory();
}