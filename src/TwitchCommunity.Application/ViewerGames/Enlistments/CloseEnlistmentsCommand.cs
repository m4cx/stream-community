using MediatR;
using System.Collections.Generic;

namespace TwitchCommunity.Application.Enlistments
{
    public sealed class CloseEnlistmentsCommand : IRequest<CloseEnlistmentsCommandResponse>
    {
        public CloseEnlistmentsCommand(IEnumerable<int> enlistmentIds)
        {
            EnlistmentIds = enlistmentIds;
        }

        public IEnumerable<int> EnlistmentIds { get; }
    }
}
