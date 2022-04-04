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
internal sealed class PlayerEnlistmentFailedEventHandler : ChatMessageEventHandlerBase, INotificationHandler<PlayerEnlistmentFailed>
{
    private static readonly ChatMessageTemplate DefaultResponseMessage = new (
        ChatMessageTemplateIdentifiers.PlayerEnlistedEventHandler,
        "Viewer Game konnte nicht für {UserName} vorgemerkt werden. {Reason}");

    private readonly IChatMessaging messaging;

    public PlayerEnlistmentFailedEventHandler(IStreamCommunityContext communityContext, IChatMessaging messaging)
        : base(communityContext)
    {
        this.messaging = messaging ?? throw new ArgumentNullException(nameof(messaging));
    }

    protected override string ChatMessageTemplateIdentifier => ChatMessageTemplateIdentifiers.PlayerEnlistmentFailedEventHandler;

    public async Task Handle(PlayerEnlistmentFailed notification, CancellationToken cancellationToken)
    {
        var messageTemplate = await GetChatMessageTemplateAsync(cancellationToken) ?? DefaultResponseMessage;
        var replacements = new Dictionary<string, string>
        {
            { "{UserName}", notification.UserName },
            { "{Reason}", notification.Reason }
        };

        await messaging.SendMessageAsync(messageTemplate.Replace(replacements));
    }
}