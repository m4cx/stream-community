using System.Collections.Generic;
using MediatR;

namespace StreamCommunity.Application.ViewerGames.Enlistments
{
    public sealed class DrawEnlistmentsCommand : IRequest
    {
        public DrawEnlistmentsCommand(IEnumerable<int> selectedEnlistmentIds)
        {
            SelectedEnlistmentIds = selectedEnlistmentIds;
        }

        public IEnumerable<int> SelectedEnlistmentIds { get; }
    }
}
