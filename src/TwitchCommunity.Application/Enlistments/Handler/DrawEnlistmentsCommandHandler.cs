using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TwitchCommunity.Application.Persistence;

namespace TwitchCommunity.Application.Enlistments.Handler
{
    internal sealed class DrawEnlistmentsCommandHandler : IRequestHandler<DrawEnlistmentsCommand>
    {
        private readonly ITwitchCommunityContext communityContext;

        public DrawEnlistmentsCommandHandler(ITwitchCommunityContext communityContext)
        {
            this.communityContext = communityContext;
        }

        public async Task<Unit> Handle(DrawEnlistmentsCommand request, CancellationToken cancellationToken)
        {
            var selectedEnlistments = communityContext.Enlistments.Where(x => request.SelectedEnlistmentIds.Contains(x.Id));
            foreach (var enlistment in selectedEnlistments)
            {
                enlistment.State = EnlistmentState.Active;
            }

            await communityContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
