using OzonTestMailSender.Core.Exceptions;

namespace OzonTestMailSender.Core.Models;

public class EmailMessage
{
    public string Recipient { get; }
    public string Subject { get; }
    public string Text { get; }
    public string[] CarbonCopyRecipients { get; }

    public EmailMessage(string recipient, string subject, string text, string[] carbonCopyRecipients)
    {
        //TODO check recipient for correct email address
        if (string.IsNullOrWhiteSpace(recipient))
            throw new ValidationException(nameof(recipient));
        Recipient = recipient;

        Subject = subject ?? throw new ValidationException(nameof(subject));
        
        Text = text ?? throw new ValidationException(nameof(text));

        //TODO check CarbonCopyRecipients for correct email address
        CarbonCopyRecipients = carbonCopyRecipients;
    }

    public EmailMessage(string recipient, string subject, string text)
        : this(recipient, subject, text, Array.Empty<string>())
    {
        
    }
}