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
    public void ConstructWithExceptionResult(string recipient, string subject, string text)
    {
        var carbonCopyRecipients = Array.Empty<string>();
        
        Action act = () => new EmailMessage(recipient, subject, text, carbonCopyRecipients);

        act.Should().Throw<ValidationException>();
    }
}