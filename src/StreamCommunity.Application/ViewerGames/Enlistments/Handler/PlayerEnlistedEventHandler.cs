using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using StreamCommunity.Application.Common;
using StreamCommunity.Application.ViewerGames.Enlistments.Events;

namespace StreamCommunity.Application.ViewerGames.Enlistments.Handler;

[UsedImplicitly]
internal sealed class PlayerEnlistedEventHandler : INotificationHandler<PlayerEnlisted>
{
    private readonly IChatMessaging messaging;

    public PlayerEnlistedEventHandler(IChatMessaging messaging)
    {
        this.messaging = messaging ?? throw new ArgumentNullException(nameof(messaging));
    }
    
    public async Task Handle(PlayerEnlisted notification, CancellationToken cancellationToken)
    {
        await messaging.SendMessageAsync(
            $"Hey {notification.UserName}, du wurdest erfolgreich für ein Viewer Game vorgemerkt");
    }
}