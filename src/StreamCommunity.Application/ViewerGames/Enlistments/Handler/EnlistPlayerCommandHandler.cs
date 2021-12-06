﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using StreamCommunity.Application.Common;
using StreamCommunity.Application.Enlistments;
using StreamCommunity.Application.Persistence;
using StreamCommunity.Domain;

namespace StreamCommunity.Application.ViewerGames.Enlistments.Handler
{
    internal sealed class EnlistPlayerCommandHandler : IRequestHandler<EnlistPlayerCommand>
    {
        private readonly ITwitchCommunityContext twitchCommunityContext;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IMediator mediator;
        private readonly ILogger<EnlistPlayerCommandHandler> logger;

        public EnlistPlayerCommandHandler(
            ITwitchCommunityContext twitchCommunityContext,
            IDateTimeProvider dateTimeProvider,
            IMediator mediator,
            ILogger<EnlistPlayerCommandHandler> logger)
        {
            this.twitchCommunityContext = twitchCommunityContext;
            this.dateTimeProvider = dateTimeProvider;
            this.mediator = mediator;
            this.logger = logger;
        }

        public async Task<Unit> Handle(EnlistPlayerCommand request, CancellationToken cancellationToken)
        {
            // check if an open or active enlistment exists
            if (twitchCommunityContext.Enlistments.Any(
                x => x.UserName == request.UserName
                     && (x.State == EnlistmentState.Active || x.State == EnlistmentState.Open)))
            {
                logger.LogInformation("User {userName} has already an open or active enlistment", request.UserName);
                return Unit.Value;
            }

            var enlistment = new Enlistment(request.UserName, dateTimeProvider.UtcNow);
            twitchCommunityContext.Enlistments.Add(enlistment);
            await twitchCommunityContext.SaveChangesAsync(cancellationToken);

            await mediator.Publish(new PlayerEnlisted(request.UserName), cancellationToken);

            return Unit.Value;
        }
    }
}