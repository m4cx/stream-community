using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StreamCommunity.Application.Enlistments;
using StreamCommunity.Application.Persistence;
using StreamCommunity.Domain;

namespace StreamCommunity.Application.ViewerGames.Enlistments.Handler
{
    [UsedImplicitly]
    internal sealed class GetEnlistmentsRequestHandler : IRequestHandler<GetEnlistmentsRequest, GetEnlistmentsResponse>
    {
        private readonly ITwitchCommunityContext communityContext;

        public GetEnlistmentsRequestHandler(ITwitchCommunityContext enlistmentRepository)
        {
            this.communityContext = enlistmentRepository;
        }

        public async Task<GetEnlistmentsResponse> Handle(
            GetEnlistmentsRequest request,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Enlistment> query = communityContext.Enlistments;
            if (request.State.HasValue)
            {
                query = query.Where(x => x.State == request.State.Value);
            }
            else
            {
                query = query.Where(x => x.State == EnlistmentState.Open || x.State == EnlistmentState.Active);
            }

            var enlistments = await query.ToArrayAsync(cancellationToken);
            return new GetEnlistmentsResponse(enlistments);
        }
    }
}