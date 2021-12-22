using System.Collections.Generic;
using MediatR;

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
