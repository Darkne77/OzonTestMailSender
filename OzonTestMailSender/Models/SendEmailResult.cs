using System.Text.Json.Serialization;
using OzonTestMailSender.Core.Models;

namespace OzonTestMailSender.Models;

public class SendEmailResult
{
    public string Recipient { get; set; }
    
    public string Subject { get; set; }
    
    public string Text { get; set; }
    
    //TODO .AddJsonOptions(o => o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);
    [JsonPropertyName("Carbon_Copy_Recipients")]
    public string[] CarbonCopyRecipients { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MessageStatus Status { get; set; }
}