using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TwitchCommunity.Application.Persistence;

namespace TwitchCommunity.Application.Enlistments.Handler
{
    internal sealed class GetEnlistmentsRequestHandler : IRequestHandler<GetEnlistmentsRequest, GetEnlistmentsResponse>
    {
        private readonly ITwitchCommunityContext communityContext;

        public GetEnlistmentsRequestHandler(ITwitchCommunityContext enlistmentRepository)
        {
            this.communityContext = enlistmentRepository;
        }

        public async Task<GetEnlistmentsResponse> Handle(GetEnlistmentsRequest request, CancellationToken cancellationToken = default)
        {
            var enlistments = await communityContext.Enlistments.ToArrayAsync(cancellationToken);

            return new GetEnlistmentsResponse(enlistments);
        }
    }
}
