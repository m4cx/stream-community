using MediatR;
using System.Collections.Generic;

namespace TwitchCommunity.Application.Enlistments
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
