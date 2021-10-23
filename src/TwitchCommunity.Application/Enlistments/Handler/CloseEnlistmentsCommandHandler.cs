using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TwitchCommunity.Application.Persistence;

namespace TwitchCommunity.Application.Enlistments.Handler
{
    internal sealed class CloseEnlistmentsCommandHandler : IRequestHandler<CloseEnlistementsCommand, CloseEnlistmentsCommandResponse>
    {
        private readonly ITwitchCommunityContext communityContext;

        public CloseEnlistmentsCommandHandler(ITwitchCommunityContext communityContext)
        {
            this.communityContext = communityContext;
        }

        public async Task<CloseEnlistmentsCommandResponse> Handle(
            CloseEnlistementsCommand request, 
            CancellationToken cancellationToken)
        {
            var enlistmentsToClose = await communityContext.Enlistments
                .Where(x => request.EnlistmentIds.Contains(x.Id))
                .ToListAsync();

            foreach(var enlistment in enlistmentsToClose)
            {
                enlistment.Close();
            }

            await communityContext.SaveChangesAsync(cancellationToken);

            return new CloseEnlistmentsCommandResponse();
        }
    }
}
