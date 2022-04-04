using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StreamCommunity.Application.Persistence;
using StreamCommunity.Application.ViewerGames.Events;
using StreamCommunity.Domain;

namespace StreamCommunity.Application.ViewerGames.Handler;

[UsedImplicitly]
internal sealed class WithdrawPlayerCommandHandler : IRequestHandler<WithdrawPlayerCommand>
{
    private readonly IStreamCommunityContext communityContext;
    private readonly IMediator mediator;

    public WithdrawPlayerCommandHandler(IStreamCommunityContext communityContext, IMediator mediator)
    {
        this.communityContext = communityContext ?? throw new ArgumentNullException(nameof(communityContext));
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<Unit> Handle(WithdrawPlayerCommand request, CancellationToken cancellationToken)
    {
        var enlistment = await communityContext.Enlistments
            .SingleOrDefaultAsync(
                x => x.UserName == request.UserName && x.State == EnlistmentState.Open,
                cancellationToken);
        if (enlistment == null)
        {
            var notification = new PlayerWithdrawelFailed(
                request.UserName,
                PlayerWithdrawelFailedReason.PlayerNotFound);
            await mediator.Publish(notification, cancellationToken);

            return Unit.Value;
        }

        enlistment.Withdraw();
        await communityContext.SaveChangesAsync(cancellationToken);

        var successNotification = new PlayerWithdrawn(request.UserName);
        await mediator.Publish(successNotification, cancellationToken);

        return Unit.Value;
    }
}