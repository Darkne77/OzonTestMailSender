using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using OzonTestMailSender.Core.Models;
using OzonTestMailSender.Core.Services;
using OzonTestMailSender.Infrastructure.Models;

namespace OzonTestMailSender.Infrastructure.Services;

public class EmailSender : IEmailSender
{
    private readonly SmtpClient _client;
    private readonly SenderCredentials _credentials;
    
    public EmailSender(IOptions<SenderCredentials> credentials)
    {
        _credentials = credentials.Value;
        _client = new SmtpClient()
                  {
                      Host = "smtp.gmail.com",
                      Port = 587,
                      Credentials = new NetworkCredential(_credentials.Name, _credentials.Password),
                      EnableSsl = true,
                  };
    }
    
    public async Task Send(EmailMessage emailMessage, CancellationToken token)
    {
        var message = new MailMessage(_credentials.Name,
                                      emailMessage.Recipient,
                                      emailMessage.Subject,
                                      emailMessage.Text);
        foreach (var recipient in emailMessage.CarbonCopyRecipients)
        {
            if (!recipient.Equals(emailMessage.Recipient))
            {
                message.To.Add(recipient);
            }
        }
        await _client.SendMailAsync(message, token);
    }
}