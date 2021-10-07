using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchCommunity.Application.Enlistments.Handler
{
    internal sealed class GetEnlistmentsRequestHandler : IRequestHandler<GetEnlistmentsRequest, GetEnlistmentsResponse>
    {
        private readonly IEnlistmentRepository enlistmentRepository;

        public GetEnlistmentsRequestHandler(IEnlistmentRepository enlistmentRepository)
        {
            this.enlistmentRepository = enlistmentRepository;
        }

        public async Task<GetEnlistmentsResponse> Handle(GetEnlistmentsRequest request, CancellationToken cancellationToken = default)
        {
            var enlistments = await enlistmentRepository.GetAllAsync(cancellationToken);

            return new GetEnlistmentsResponse(enlistments);
        }
    }
}
