using AutoMapper;
using OzonTestMailSender.Core.Models;
using OzonTestMailSender.Models;

namespace OzonTestMailSender.MappingProfiles;

public class SendEmailResultProfile : Profile
{
    public SendEmailResultProfile()
    {
        CreateMap<SentMessageResult, SendEmailResult>()
            .ForMember(m => m.Recipient, c => c.MapFrom(p => p.EmailMessage.Recipient))
            .ForMember(m => m.Subject, c => c.MapFrom(p => p.EmailMessage.Subject))
            .ForMember(m => m.Text, c => c.MapFrom(p => p.EmailMessage.Text))
            .ForMember(m => m.CarbonCopyRecipients, c => c.MapFrom(p => p.EmailMessage.CarbonCopyRecipients))
            .ForMember(m => m.Status, c => c.MapFrom(p => p.Status));
    }
}