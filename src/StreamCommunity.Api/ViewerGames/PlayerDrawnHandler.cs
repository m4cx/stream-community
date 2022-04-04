using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using StreamCommunity.Application.ViewerGames.Events;

namespace StreamCommunity.Api.ViewerGames
{
    [UsedImplicitly]
    internal sealed class PlayerDrawnHandler : INotificationHandler<PlayerDrawn>
    {
        private readonly IHubContext<ApiHub> hubContext;

        public PlayerDrawnHandler(IHubContext<ApiHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public async Task Handle(PlayerDrawn notification, CancellationToken cancellationToken)
        {
            await hubContext.Clients.All.SendAsync("notify", notification, cancellationToken: cancellationToken);
        }
    }
}