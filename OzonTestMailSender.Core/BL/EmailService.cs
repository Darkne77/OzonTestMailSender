using OzonTestMailSender.Core.Exceptions;
using OzonTestMailSender.Core.Models;
using OzonTestMailSender.Core.Repositories;
using OzonTestMailSender.Core.Services;

namespace OzonTestMailSender.Core.BL;

public class EmailService : IEmailService
{
    private readonly IEmailSender _emailSender;
    private readonly IMessageHistoryRepository _messageHistory;

    //TODO Add cancellation token
    public EmailService(IEmailSender emailSender, IMessageHistoryRepository messageHistory)
    {
        _emailSender = emailSender;
        _messageHistory = messageHistory;
    }

    public async Task<IEnumerable<SentMessageResult>> GetMessageHistory()
    {
        try
        {
            return await _messageHistory.GetAll();
        }
        catch (Exception e)
        {
            //TODO Custom exception
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task Send(EmailMessage message)
    {
        try
        {
            await _emailSender.Send(message);
        }
        catch (Exception e)
        {
            await SaveMessageResult(message, MessageStatus.SendingError);
            throw new SendEmailMessageException();
        }

        try
        {
            await SaveMessageResult(message, MessageStatus.Sent);
        }
        catch
        {
            //TODO log error
        }
    }

    private async Task SaveMessageResult(EmailMessage message, MessageStatus status)
    {
        var messageResult = new SentMessageResult(message, status);
        await _messageHistory.Add(messageResult);
    }
}