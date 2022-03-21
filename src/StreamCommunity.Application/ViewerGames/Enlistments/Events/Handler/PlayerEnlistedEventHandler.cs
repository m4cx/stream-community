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

namespace StreamCommunity.Application.ViewerGames.Enlistments.Handler;

[UsedImplicitly]
internal sealed class PlayerEnlistedEventHandler : INotificationHandler<PlayerEnlisted>
{
    private static readonly ChatMessageTemplate DefaultResponseMessage =
        new ChatMessageTemplate(
            ChatMessageTemplateIdentifiers.PlayerEnlistedEventHandler,
            "Viewer Game erfolgreich für {UserName} vorgemerkt.");

    private readonly IStreamCommunityContext communityContext;
    private readonly IChatMessaging messaging;

    public PlayerEnlistedEventHandler(IStreamCommunityContext communityContext, IChatMessaging messaging)
    {
        this.communityContext = communityContext ?? throw new ArgumentNullException(nameof(communityContext));
        this.messaging = messaging ?? throw new ArgumentNullException(nameof(messaging));
    }

    public async Task Handle(PlayerEnlisted notification, CancellationToken cancellationToken)
    {
        var messageTemplate = await communityContext.ChatMessageTemplates
            .SingleOrDefaultAsync(
                x => x.Identifier == ChatMessageTemplateIdentifiers.PlayerEnlistedEventHandler,
                cancellationToken);
        messageTemplate ??= DefaultResponseMessage;

        var replacements = new Dictionary<string, string>
        {
            { "{UserName}", notification.UserName }
        };

        var message = messageTemplate?.Replace(replacements);
        await messaging.SendMessageAsync(message);
    }
}