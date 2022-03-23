using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using StreamCommunity.Application.ViewerGames.Enlistments.Events;

namespace StreamCommunity.Api.ViewerGames;

[UsedImplicitly]
internal sealed class PlayerWithdrawHandler : INotificationHandler<PlayerWithdrawn>
{
    private readonly IHubContext<ApiHub> hubContext;

    public PlayerWithdrawHandler(IHubContext<ApiHub> hubContext)
    {
        this.hubContext = hubContext;
    }

    public async Task Handle(PlayerWithdrawn notification, CancellationToken cancellationToken)
    {
        await hubContext.Clients.All.SendAsync("notify", notification, cancellationToken);
    }
}