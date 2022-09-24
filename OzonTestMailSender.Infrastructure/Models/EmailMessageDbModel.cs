using OzonTestMailSender.Core.Models;

namespace OzonTestMailSender.Infrastructure.Models;

public class EmailMessageDbModel
{
    public int Id { get; }
    public string Recipient { get; }
    public string Subject { get; }
    public string Text { get; }
    public string[] CarbonCopyRecipients { get; }
    public MessageStatus Status { get; }

    public EmailMessageDbModel(int id, string recipient, string subject, string text, Array carbonCopyRecipients, MessageStatus status)
    {
        Id = id;
        Recipient = recipient;
        Subject = subject;
        Text = text;
        CarbonCopyRecipients = (string[])carbonCopyRecipients;
        Status = status;
    }

    public SentMessageResult ToDomain()
    {
        return new SentMessageResult(new EmailMessage(Recipient, Subject, Text, CarbonCopyRecipients), Status);
    }
}