namespace OzonTestMailSender.Models;

public class GetEmailsHistoryResponse
{
    public IEnumerable<SendEmailResult> SendEmailResults { get; }

    public GetEmailsHistoryResponse(IEnumerable<SendEmailResult> sendEmailResults)
    {
        SendEmailResults = sendEmailResults;
    }
}