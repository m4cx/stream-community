﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StreamCommunity.Application.Persistence;
using StreamCommunity.Domain;

namespace StreamCommunity.Application.ViewerGames.Enlistments.Handler
{
    [UsedImplicitly]
    internal sealed class GetEnlistmentsRequestHandler : IRequestHandler<GetEnlistmentsRequest, GetEnlistmentsResponse>
    {
        private readonly IStreamCommunityContext communityContext;

        public GetEnlistmentsRequestHandler(IStreamCommunityContext enlistmentRepository)
        {
            communityContext = enlistmentRepository;
        }

        public async Task<GetEnlistmentsResponse> Handle(
            GetEnlistmentsRequest request,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Enlistment> query = communityContext.Enlistments;
            query = request.State.HasValue
                ? query.Where(x => x.State == request.State.Value)
                : query.Where(x => x.State == EnlistmentState.Open || x.State == EnlistmentState.Active);

            var enlistments = await query.ToArrayAsync(cancellationToken);
            return new GetEnlistmentsResponse(enlistments);
        }
    }
}