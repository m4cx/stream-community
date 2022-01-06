using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StreamCommunity.Application.Persistence;

namespace StreamCommunity.Application.ViewerGames.Enlistments.Handler
{
    [UsedImplicitly]
    internal sealed class
        CloseEnlistmentsCommandHandler : IRequestHandler<CloseEnlistmentsCommand, CloseEnlistmentsCommandResponse>
    {
        private readonly IStreamCommunityContext communityContext;

        public CloseEnlistmentsCommandHandler(IStreamCommunityContext communityContext)
        {
            this.communityContext = communityContext;
        }

        public async Task<CloseEnlistmentsCommandResponse> Handle(
            CloseEnlistmentsCommand request,
            CancellationToken cancellationToken)
        {
            var enlistmentsToClose = await communityContext.Enlistments
                .Where(x => request.EnlistmentIds.Contains(x.Id))
                .ToListAsync(cancellationToken: cancellationToken);

            foreach (var enlistment in enlistmentsToClose)
            {
                enlistment.Close();
            }

            await communityContext.SaveChangesAsync(cancellationToken);

            return new CloseEnlistmentsCommandResponse();
        }
    }
}