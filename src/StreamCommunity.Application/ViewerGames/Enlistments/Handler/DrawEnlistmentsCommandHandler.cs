using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using StreamCommunity.Application.Persistence;

namespace StreamCommunity.Application.ViewerGames.Enlistments.Handler
{
    [UsedImplicitly]
    internal sealed class DrawEnlistmentsCommandHandler : IRequestHandler<DrawEnlistmentsCommand>
    {
        private readonly ITwitchCommunityContext communityContext;

        public DrawEnlistmentsCommandHandler(ITwitchCommunityContext communityContext)
        {
            this.communityContext = communityContext;
        }

        public async Task<Unit> Handle(DrawEnlistmentsCommand request, CancellationToken cancellationToken)
        {
            var selectedEnlistments = communityContext.Enlistments
                .Where(x => request.SelectedEnlistmentIds.Contains(x.Id));
            foreach (var enlistment in selectedEnlistments)
            {
                enlistment.Draw();
            }

            await communityContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
