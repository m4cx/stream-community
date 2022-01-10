using System.Collections.Generic;

namespace StreamCommunity.Domain;

public sealed class ChatMessageTemplate
{
    public ChatMessageTemplate(string identifier, string message)
    {
        Id = 0;
        Identifier = identifier;
        Message = message;
    }

    public int Id { get; private set; }

    public string Identifier { get; }

    public string Message { get; set; }

    public string Replace(IDictionary<string, string> replacements)
    {
        var result = Message;
        foreach (var (placeholder, value) in replacements)
        {
            if (result.Contains(placeholder))
            {
                result = result.Replace(placeholder, value);
            }
        }

        return result;
    }
}