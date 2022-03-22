using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StreamCommunity.Application.ChatMessages;
using StreamCommunity.Application.Common;
using StreamCommunity.Application.Persistence;
using StreamCommunity.Application.ViewerGames.Enlistments.Events;
using StreamCommunity.Domain;

namespace StreamCommunity.Application.ViewerGames.Enlistments.EventHandler;

[UsedImplicitly]
internal sealed class PlayerEnlistmentFailedEventHandler : INotificationHandler<PlayerEnlistmentFailed>
{
    private static readonly ChatMessageTemplate DefaultResponseMessage = new(
        ChatMessageTemplateIdentifiers.PlayerEnlistedEventHandler,
        "Viewer Game konnte nicht für {UserName} vorgemerkt werden. {Reason}");

    private readonly IStreamCommunityContext communityContext;
    private readonly IChatMessaging messaging;

    public PlayerEnlistmentFailedEventHandler(IStreamCommunityContext communityContext, IChatMessaging messaging)
    {
        this.communityContext = communityContext ?? throw new ArgumentNullException(nameof(communityContext));
        this.messaging = messaging ?? throw new ArgumentNullException(nameof(messaging));
    }

    public async Task Handle(PlayerEnlistmentFailed notification, CancellationToken cancellationToken)
    {
        var messageTemplate = await communityContext.ChatMessageTemplates
            .SingleOrDefaultAsync(
                x => x.Identifier == ChatMessageTemplateIdentifiers.PlayerEnlistmentFailedEventHandler,
                cancellationToken);
        messageTemplate ??= DefaultResponseMessage;

        var replacements = new Dictionary<string, string>
        {
            {"{UserName}", notification.UserName},
            {"{Reason}", notification.Reason}
        };

        await messaging.SendMessageAsync(messageTemplate.Replace(replacements));
    }
}
