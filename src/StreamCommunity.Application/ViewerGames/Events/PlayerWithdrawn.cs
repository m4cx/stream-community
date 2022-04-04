using System;
using MediatR;

namespace StreamCommunity.Application.ViewerGames.Events;

public sealed class PlayerWithdrawn : EventBase, INotification
{
    public PlayerWithdrawn(string userName)
    {
        UserName = userName ?? throw new ArgumentNullException(nameof(userName));
    }

    public string UserName { get; }

    public override string Type => EventNames.PlayerWithdrawn;
}