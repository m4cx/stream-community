using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using StreamCommunity.Application.ChatMessages;
using StreamCommunity.Application.Common;
using StreamCommunity.Application.Persistence;
using StreamCommunity.Application.ViewerGames.Enlistments.Events;
using StreamCommunity.Domain;

namespace StreamCommunity.Application.ViewerGames.Enlistments.EventHandler;

[UsedImplicitly]
internal sealed class PlayerEnlistedEventHandler : ChatMessageEventHandlerBase, INotificationHandler<PlayerEnlisted>
{
    private static readonly ChatMessageTemplate DefaultResponseMessage =
        new ChatMessageTemplate(
            ChatMessageTemplateIdentifiers.PlayerEnlistedEventHandler,
            "Viewer Game erfolgreich für {UserName} vorgemerkt.");

    private readonly IChatMessaging messaging;

    public PlayerEnlistedEventHandler(IStreamCommunityContext communityContext, IChatMessaging messaging)
        : base(communityContext)
    {
        this.messaging = messaging ?? throw new ArgumentNullException(nameof(messaging));
    }

    protected override string ChatMessageTemplateIdentifier => ChatMessageTemplateIdentifiers.PlayerEnlistedEventHandler;

    public async Task Handle(PlayerEnlisted notification, CancellationToken cancellationToken)
    {
        var messageTemplate = await GetChatMessageTemplateAsync(cancellationToken) ?? DefaultResponseMessage;
        var replacements = new Dictionary<string, string>
        {
            { "{UserName}", notification.UserName }
        };

        var message = messageTemplate?.Replace(replacements);
        await messaging.SendMessageAsync(message);
    }
}