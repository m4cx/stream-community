using System.Collections.Generic;
using NUnit.Framework;

namespace StreamCommunity.Domain.Tests;

[TestFixture]
public class ChatMessageTemplateTest
{
    [Test]
    public void Ctor_WithIdentifierValue_PropertyReturnsIdentifier()
    {
        var messageTemplate = new ChatMessageTemplate("id", "message");
        Assert.AreEqual("id", messageTemplate.Identifier);
    }
    
    [TestCaseSource(nameof(MySourceMethod))]
    public void Replace_WithNoPlaceholderAndEmptyReplacements_ReturnsOriginalString(string original, string expected,
        IDictionary<string, string> replacements)
    {
        var messageTemplate = new ChatMessageTemplate("id", original);
        Assert.AreEqual(expected, messageTemplate.Replace(replacements));
    }

    private static IEnumerable<object?[]> MySourceMethod()
    {
        return new[]
        {
            new object?[] { null, null, new Dictionary<string, string>() },
            new object?[] { "", "", new Dictionary<string, string>() },
            new object?[] { "Hallo du", "Hallo du", new Dictionary<string, string>() },
            new object?[] { "Hallo {UserName}", "Hallo {UserName}", new Dictionary<string, string>() },
            new object?[]
                { "Hallo {UserName}", "Hallo m4cx", new Dictionary<string, string> { { "{UserName}", "m4cx" } } },
        };
    }
}