using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using StreamCommunity.Application.Common;
using StreamCommunity.Application.ViewerGames.Enlistments.Events;

namespace StreamCommunity.Application.ViewerGames.Enlistments.Handler;

[UsedImplicitly]
internal sealed class PlayerEnlistmentFailedEventHandler : INotificationHandler<PlayerEnlistmentFailed>
{
    private readonly IChatMessaging messaging;

    public PlayerEnlistmentFailedEventHandler(IChatMessaging messaging)
    {
        this.messaging = messaging ?? throw new ArgumentNullException(nameof(messaging));
    }

    public async Task Handle(PlayerEnlistmentFailed notification, CancellationToken cancellationToken)
    {
        await messaging.SendMessageAsync($"{notification.UserName}, deine Anmeldung hat leider nicht geklappt.");
    }
}