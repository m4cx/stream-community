using System;
using MediatR;

namespace StreamCommunity.Application.ViewerGames.Enlistments
{
    public sealed class EnlistPlayerCommand : IRequest
    {
        public EnlistPlayerCommand(string userName)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        }

        public string UserName { get; }
    }
}
