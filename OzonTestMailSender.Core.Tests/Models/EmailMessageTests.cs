using FluentAssertions;
using NUnit.Framework;
using OzonTestMailSender.Core.Exceptions;
using OzonTestMailSender.Core.Models;

namespace OzonTestMailSender.Core.Tests.Models;

public class EmailMessageTests
{
    [TestCase(null, "SomeSubject", "SomeText")]
    [TestCase("", "SomeSubject", "SomeText")]
    [TestCase(" ", "SomeSubject", "SomeText")]
    public void ConstructWithInvalidRecipient(string recipient, string subject, string text)
    {
        var carbonCopyRecipients = Array.Empty<string>();
        
        Action act = () => new EmailMessage(recipient, subject, text, carbonCopyRecipients);

        act.Should().Throw<ValidationException>();
    }
    
    [TestCase("ValidRecipient@gmail.com", "SomeSubject", "SomeText",
        ExpectedResult = "ValidRecipient@gmail.com")]
    public string ConstructWithValidRecipient(string recipient, string subject, string text)
    {
        var carbonCopyRecipients = Array.Empty<string>();
        
        var message = new EmailMessage(recipient, subject, text, carbonCopyRecipients);

        return message.Recipient;
    }

    [TestCase("SomeRecipient@gmail.com", null, "SomeText")]
    public void ConstructWithInvalidSubject(string recipient, string subject, string text)
    {
        var carbonCopyRecipients = Array.Empty<string>();
        
        Action act = () => new EmailMessage(recipient, subject, text, carbonCopyRecipients);

        act.Should().Throw<ValidationException>();
    }
    
    [TestCase("SomeRecipient@gmail.com", "ValidSubject", "SomeText",
        ExpectedResult = "ValidSubject")]
    public string ConstructWithValidSubject(string recipient, string subject, string text)
    {
        var carbonCopyRecipients = Array.Empty<string>();
        
        var message = new EmailMessage(recipient, subject, text, carbonCopyRecipients);

        return message.Subject;
    }
    
    [TestCase("SomeRecipient@gmail.com", "SomeSubject", null)]
    public void ConstructWithInvalidText(string recipient, string subject, string text)
    {
        var carbonCopyRecipients = Array.Empty<string>();
        
        Action act = () => new EmailMessage(recipient, subject, text, carbonCopyRecipients);

        act.Should().Throw<ValidationException>();
    }
    
    [TestCase("SomeRecipient@gmail.com", "SomeSubject", "ValidText",
        ExpectedResult = "ValidText")]
    public string ConstructWithValidText(string recipient, string subject, string text)
    {
        var carbonCopyRecipients = Array.Empty<string>();
        
        var message = new EmailMessage(recipient, subject, text, carbonCopyRecipients);

        return message.Text;
    }
    
    [TestCase("SomeRecipient@gmail.com", "SomeSubject", "SomeText", new []{"rec1, rec2"},
        ExpectedResult = new []{"rec1, rec2"})]
    [TestCase("SomeRecipient@gmail.com", "SomeSubject", "SomeText", null,
        ExpectedResult = null)]
    public string[] ConstructWithValidCarbonCopyRecipientsValues(string recipient, string subject, string text,
        string[] carbonCopyRecipients)
    {
        var message = new EmailMessage(recipient, subject, text, carbonCopyRecipients);

        return message.CarbonCopyRecipients;
    }
}