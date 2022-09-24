using OzonTestMailSender.Core.Models;

namespace OzonTestMailSender.Core.Services;

public interface IEmailSender
{
    Task Send(EmailMessage emailMessage, CancellationToken token);
}