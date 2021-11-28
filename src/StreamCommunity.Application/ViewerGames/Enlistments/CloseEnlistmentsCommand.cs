using System.Collections.Generic;
using MediatR;
using StreamCommunity.Application.Enlistments;

namespace StreamCommunity.Application.ViewerGames.Enlistments
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
