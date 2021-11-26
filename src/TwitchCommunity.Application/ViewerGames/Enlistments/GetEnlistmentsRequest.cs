using MediatR;
using TwitchCommunity.Domain;

namespace TwitchCommunity.Application.Enlistments
{
    public sealed class GetEnlistmentsRequest : IRequest<GetEnlistmentsResponse>
    {
        public GetEnlistmentsRequest(EnlistmentState? state = null)
        {
            State = state;
        }

        public EnlistmentState? State { get; }
    }
}
