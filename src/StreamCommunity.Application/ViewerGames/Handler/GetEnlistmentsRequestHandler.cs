using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StreamCommunity.Application.Persistence;
using StreamCommunity.Domain;

namespace StreamCommunity.Application.ViewerGames.Handler;

[UsedImplicitly]
internal sealed class GetEnlistmentsRequestHandler : IRequestHandler<GetEnlistmentsRequest, GetEnlistmentsResponse>
{
    private readonly IStreamCommunityContext communityContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetEnlistmentsRequestHandler"/> class.
    /// </summary>
    /// <param name="communityContext">Access to the persistence layer.</param>
    public GetEnlistmentsRequestHandler(IStreamCommunityContext communityContext)
    {
        this.communityContext = communityContext;
    }

    /// <inheritdoc cref="IRequestHandler{TRequest,TResponse}.Handle"/>
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