using MediatR;
using StreamCommunity.Application.ViewerGames.Enlistments;
using StreamCommunity.Domain;

namespace StreamCommunity.Application.Enlistments
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
