using FluentResults;
using OzonTestMailSender.Core.Errors;
using OzonTestMailSender.Core.Exceptions;
using OzonTestMailSender.Core.Models;
using OzonTestMailSender.Core.Repositories;
using OzonTestMailSender.Core.Services;

namespace OzonTestMailSender.Core.BL;

public class EmailService : IEmailService
{
    private readonly IEmailSender _emailSender;
    private readonly IMessageHistoryRepository _messageHistory;
    
    public EmailService(IEmailSender emailSender, IMessageHistoryRepository messageHistory)
    {
        _emailSender = emailSender;
        _messageHistory = messageHistory;
    }

    public async Task<Result<IEnumerable<SentMessageResult>>> GetMessageHistory()
    {
        try
        {
            return Result.Ok(await _messageHistory.GetAll());
        }
        catch (Exception e)
        {
            return Result.Fail(new GetMessageHistoryError().CausedBy(e));
        }
    }

    public async Task Send(EmailMessage message, CancellationToken token)
    {
        //TODO use Result pattern
        try
        {
            await _emailSender.Send(message, token);
        }
        catch (Exception e)
        {
            await SaveMessageResult(message, MessageStatus.SendingError, token);
            throw new SendEmailMessageException();
        }

        try
        {
            await SaveMessageResult(message, MessageStatus.Sent, token);
        }
        catch
        {
            //TODO log error
        }
    }

    private async Task SaveMessageResult(EmailMessage message, MessageStatus status,
        CancellationToken token)
    {
        var messageResult = new SentMessageResult(message, status);
        await _messageHistory.Add(messageResult, token);
    }
}