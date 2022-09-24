using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OzonTestMailSender.Models;

public class SendEmailMessageRequest
{
    [Required(AllowEmptyStrings = false)]
    public string Recipient { get; set; }
    
    [Required]
    public string Subject { get; set; }
    
    [Required]
    public string Text { get; set; }
    
    public string[] CarbonCopyRecipients { get; set; }
}