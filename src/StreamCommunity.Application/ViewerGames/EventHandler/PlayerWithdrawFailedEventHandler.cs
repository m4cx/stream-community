using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using StreamCommunity.Application.ChatMessages;
using StreamCommunity.Application.Common;
using StreamCommunity.Application.Persistence;
using StreamCommunity.Application.ViewerGames.Events;
using StreamCommunity.Domain;

namespace StreamCommunity.Application.ViewerGames.EventHandler;

[UsedImplicitly]
internal sealed class PlayerWithdrawFailedEventHandler : ChatMessageEventHandlerBase, INotificationHandler<PlayerWithdrawelFailed>
{
    private static readonly ChatMessageTemplate DefaultResponseMessage = new (
        ChatMessageTemplateIdentifiers.PlayerWithdrawFailedEventHandler,
        "Viewer Game konnte nicht für {UserName} zurückgezogen werden. {Reason}");

    private readonly IChatMessaging chatMessaging;

    public PlayerWithdrawFailedEventHandler(IStreamCommunityContext communityContext, IChatMessaging chatMessaging)
        : base(communityContext)
    {
        this.chatMessaging = chatMessaging;
    }

    protected override string ChatMessageTemplateIdentifier => ChatMessageTemplateIdentifiers.PlayerWithdrawFailedEventHandler;

    public async Task Handle(PlayerWithdrawelFailed notification, CancellationToken cancellationToken)
    {
        var messageTemplate = await GetChatMessageTemplateAsync(cancellationToken) ?? DefaultResponseMessage;
        var replacements = new Dictionary<string, string>
        {
            { "{UserName}", notification.UserName },
            { "{Reason}", Enum.GetName(notification.Reason) }
        };

        await chatMessaging.SendMessageAsync(messageTemplate.Replace(replacements));
    }
}