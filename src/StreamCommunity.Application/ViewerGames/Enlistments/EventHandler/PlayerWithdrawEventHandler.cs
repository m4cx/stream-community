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
internal sealed class PlayerWithdrawEventHandler : ChatMessageEventHandlerBase, INotificationHandler<PlayerWithdrawn>
{
    private static readonly ChatMessageTemplate DefaultResponseMessage
        = new(
            ChatMessageTemplateIdentifiers.PlayerWithdrawnEventHandler,
            "Viewer Game für {UserName} wurde zurückgezogen.");

    private readonly IChatMessaging messaging;

    public PlayerWithdrawEventHandler(IStreamCommunityContext communityContext, IChatMessaging messaging)
        : base(communityContext)
    {
        this.messaging = messaging;
    }

    protected override string ChatMessageTemplateIdentifier => ChatMessageTemplateIdentifiers.PlayerWithdrawnEventHandler;

    public async Task Handle(PlayerWithdrawn notification, CancellationToken cancellationToken)
    {
        var messageTemplate = await GetChatMessageTemplateAsync(cancellationToken);
        messageTemplate ??= DefaultResponseMessage!;

        var replacements = new Dictionary<string, string>
        {
            { "{UserName}", notification.UserName }
        };

        await messaging.SendMessageAsync(messageTemplate.Replace(replacements));
    }
}