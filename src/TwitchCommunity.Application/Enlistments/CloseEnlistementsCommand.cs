using MediatR;
using System.Collections.Generic;

namespace TwitchCommunity.Application.Enlistments
{
    public sealed class CloseEnlistementsCommand : IRequest<CloseEnlistmentsCommandResponse>
    {
        public CloseEnlistementsCommand(IEnumerable<int> enlistmentIds)
        {
            EnlistmentIds = enlistmentIds;
        }

        public IEnumerable<int> EnlistmentIds { get; }
    }
}
