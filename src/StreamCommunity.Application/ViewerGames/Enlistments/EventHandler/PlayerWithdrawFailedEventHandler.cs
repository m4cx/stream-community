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
internal sealed class PlayerWithdrawFailedEventHandler : INotificationHandler<PlayerWithdrawelFailed>
{
    private static readonly ChatMessageTemplate DefaultResponseMessage = new(
        ChatMessageTemplateIdentifiers.PlayerWithdrawFailedEventHandler,
        "Viewer Game konnte nicht für {UserName} zurückgezogen werden. {Reason}");

    private readonly IStreamCommunityContext communityContext;
    private readonly IChatMessaging chatMessaging;

    public PlayerWithdrawFailedEventHandler(IStreamCommunityContext communityContext, IChatMessaging chatMessaging)
    {
        this.communityContext = communityContext;
        this.chatMessaging = chatMessaging;
    }

    public async Task Handle(PlayerWithdrawelFailed notification, CancellationToken cancellationToken)
    {
        var messageTemplate = await communityContext.ChatMessageTemplates
            .SingleOrDefaultAsync(
                x => x.Identifier == ChatMessageTemplateIdentifiers.PlayerWithdrawFailedEventHandler,
                cancellationToken);
        messageTemplate ??= DefaultResponseMessage;

        var replacements = new Dictionary<string, string>
        {
            { "{UserName}", notification.UserName },
            { "{Reason}", Enum.GetName(notification.Reason) }
        };

        await chatMessaging.SendMessageAsync(messageTemplate.Replace(replacements));
    }
}
