using System;
using System.Collections.Generic;
using MediatR;

namespace StreamCommunity.Application.ViewerGames.Enlistments
{
    public sealed class DrawEnlistmentsCommand : IRequest
    {
        public DrawEnlistmentsCommand(IEnumerable<int> selectedEnlistmentIds)
        {
            SelectedEnlistmentIds =
                selectedEnlistmentIds ?? throw new ArgumentNullException(nameof(selectedEnlistmentIds));
        }

        public IEnumerable<int> SelectedEnlistmentIds { get; }
    }
}