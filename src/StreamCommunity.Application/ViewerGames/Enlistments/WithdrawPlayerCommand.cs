using System;
using MediatR;

namespace StreamCommunity.Application.ViewerGames.Enlistments;

public class WithdrawPlayerCommand : IRequest
{
    public WithdrawPlayerCommand(string userName)
    {
        UserName = userName ?? throw new ArgumentNullException(nameof(userName));
    }

    public string UserName { get; }
}