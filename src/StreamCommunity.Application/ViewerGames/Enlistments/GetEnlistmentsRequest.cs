using MediatR;
using StreamCommunity.Domain;

namespace StreamCommunity.Application.ViewerGames.Enlistments
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
