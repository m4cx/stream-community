using System;
using MediatR;

namespace StreamCommunity.Application.ViewerGames.Enlistments.Events;

public sealed class PlayerWithdrawelFailed : INotification
{
    public PlayerWithdrawelFailed(string userName, PlayerWithdrawelFailedReason reason)
    {
        UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        Reason = reason;
    }

    public string UserName { get; }

    public PlayerWithdrawelFailedReason Reason { get; }
}