using System.Collections.Generic;

namespace StreamCommunity.Application.ChatMessages.Configuration;

public class ChatMessagesConfiguration
{
    public const string ConfigurationSectionName = "TwitchCommunity:ChatMessages";

    public List<ChatMessageTemplateOption> Templates { get; set; }
}