using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StreamCommunity.Application.Persistence;
using StreamCommunity.Domain;

namespace StreamCommunity.Application.ViewerGames.Handler;

[UsedImplicitly]
internal sealed class ChangeEnlistmentSortOrderCommandHandler : IRequestHandler<ChangeEnlistmentSortOrderCommand>
{
    private readonly IStreamCommunityContext communityContext;
    private readonly ILogger<ChangeEnlistmentSortOrderCommandHandler> logger;

    public ChangeEnlistmentSortOrderCommandHandler(IStreamCommunityContext communityContext, ILogger<ChangeEnlistmentSortOrderCommandHandler> logger)
    {
        this.communityContext = communityContext;
        this.logger = logger;
    }

    public async Task<Unit> Handle(ChangeEnlistmentSortOrderCommand request, CancellationToken cancellationToken)
    {
        var sortedOpenEnlistments = await communityContext.Enlistments
            .Where(x => x.State == EnlistmentState.Open)
            .OrderBy(x => x.SortingNo)
            .ToListAsync(cancellationToken: cancellationToken);

        if (!sortedOpenEnlistments.Any())
        {
            logger.LogDebug("Currently no open enlistments");
            return Unit.Value;
        }

        var enlistmentToChangeSort = sortedOpenEnlistments.SingleOrDefault(x => x.Id == request.EnlistmentId);
        if (enlistmentToChangeSort == null)
        {
            logger.LogError("Enlistment with Id {EnlistmentId} was not found to change sorting order", request.EnlistmentId);
            return Unit.Value;
        }

        switch (request.SortDirection)
        {
            case SortDirection.Down:
                MoveEnlistmentDown(sortedOpenEnlistments, enlistmentToChangeSort);
                break;
            case SortDirection.Up:
                MoveEnlistmentUp(sortedOpenEnlistments, enlistmentToChangeSort);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(request.SortDirection), request.SortDirection, "Unknown value for direction");
        }

        await communityContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private void MoveEnlistmentUp(IList<Enlistment> sortedOpenEnlistments, Enlistment enlistmentToChangeSort)
    {
        var indexOfEnlistment = sortedOpenEnlistments.IndexOf(enlistmentToChangeSort);
        var isFirst = indexOfEnlistment == 0;
        if (isFirst)
        {
            return;
        }

        logger.LogInformation("Moving enlistment '{EnlistmentId}' up in sorting", enlistmentToChangeSort.Id);
        var previousEnlistment = sortedOpenEnlistments[indexOfEnlistment - 1];
        (previousEnlistment.SortingNo, enlistmentToChangeSort.SortingNo) = (enlistmentToChangeSort.SortingNo, previousEnlistment.SortingNo);
    }

    private void MoveEnlistmentDown(IList<Enlistment> sortedOpenEnlistments, Enlistment enlistmentToChangeSort)
    {
        var indexOfEnlistment = sortedOpenEnlistments.IndexOf(enlistmentToChangeSort);
        var isLast = indexOfEnlistment == sortedOpenEnlistments.Count - 1;
        if (isLast)
        {
            return;
        }

        logger.LogInformation("Moving enlistment '{EnlistmentId}' down in sorting", enlistmentToChangeSort.Id);
        var nextEnlistment = sortedOpenEnlistments[indexOfEnlistment + 1];
        (nextEnlistment.SortingNo, enlistmentToChangeSort.SortingNo) = (enlistmentToChangeSort.SortingNo, nextEnlistment.SortingNo);
    }
}