using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using StreamCommunity.Application.ViewerGames.Events;

namespace StreamCommunity.Api.ViewerGames
{
    [UsedImplicitly]
    internal sealed class PlayerEnlistedHandler : INotificationHandler<PlayerEnlisted>
    {
        private readonly IHubContext<ApiHub> hubContext;

        public PlayerEnlistedHandler(IHubContext<ApiHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public async Task Handle(PlayerEnlisted notification, CancellationToken cancellationToken)
        {
            await hubContext.Clients.All.SendAsync("notify", notification, cancellationToken);
        }
    }
}