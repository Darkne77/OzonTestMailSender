namespace OzonTestMailSender.Core.Models;

public class SentMessageResult
{
    public EmailMessage EmailMessage { get; }

    public MessageStatus Status { get; }

    public SentMessageResult(EmailMessage emailMessage, MessageStatus status)
    {
        EmailMessage = emailMessage;
        Status = status;
    }
}