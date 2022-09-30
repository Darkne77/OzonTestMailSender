using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OzonTestMailSender.Core.BL;
using OzonTestMailSender.Core.Errors;
using OzonTestMailSender.Models;
using CoreEmailMessage = OzonTestMailSender.Core.Models.EmailMessage;

namespace OzonTestMailSender.Controllers;

[ApiController]
[Route("v1/api/[controller]")]
public class EmailsController : ControllerBase
{
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;

    public EmailsController(IEmailService emailService, IMapper mapper)
    {
        _emailService = emailService;
        _mapper = mapper;
    }
    
    [HttpPost]
    public async Task<IActionResult> SendEmail([FromBody]SendEmailMessageRequest model, 
        CancellationToken token)
    {
        var message = new CoreEmailMessage(model.Recipient,
                                           model.Subject,
                                           model.Text,
                                           model.CarbonCopyRecipients);
        await _emailService.Send(message, token);

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<GetEmailsHistoryResponse>> GetEmailsHistory()
    {
        var messageResults = await _emailService.GetMessageHistory();
        if (messageResults.IsFailed)
        {
            return messageResults.HasError<ApplicationError>() 
                       ? StatusCode(StatusCodes.Status500InternalServerError) 
                       : BadRequest();
        }
        var sendEmailResults = _mapper.Map<IEnumerable<SendEmailResult>>(messageResults.Value);
        return new GetEmailsHistoryResponse(sendEmailResults);
    }
}