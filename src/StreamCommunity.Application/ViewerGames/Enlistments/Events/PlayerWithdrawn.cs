using System;
using MediatR;

namespace StreamCommunity.Application.ViewerGames.Enlistments.Events;

public sealed class PlayerWithdrawn : INotification
{
    public PlayerWithdrawn(string userName)
    {
        UserName = userName ?? throw new ArgumentNullException(nameof(userName));
    }

    public string UserName { get; }
}