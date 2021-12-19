using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using StreamCommunity.Application.Persistence;
using StreamCommunity.Application.ViewerGames.Enlistments.Events;

namespace StreamCommunity.Application.ViewerGames.Enlistments.Handler
{
    [UsedImplicitly]
    internal sealed class DrawEnlistmentsCommandHandler : IRequestHandler<DrawEnlistmentsCommand>
    {
        private readonly ITwitchCommunityContext communityContext;
        private readonly IMediator mediator;

        public DrawEnlistmentsCommandHandler(
            [NotNull] ITwitchCommunityContext communityContext,
            [NotNull] IMediator mediator)
        {
            this.communityContext = communityContext ?? throw new ArgumentNullException(nameof(communityContext));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
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
            foreach (var enlistment in selectedEnlistments)
            {
                await mediator.Publish(new PlayerDrawn(enlistment.UserName), cancellationToken);
            }

            return Unit.Value;
        }
    }
}