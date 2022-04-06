using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using StreamCommunity.Application.Common;
using StreamCommunity.Application.Persistence;
using StreamCommunity.Application.ViewerGames.Events;
using StreamCommunity.Domain;

namespace StreamCommunity.Application.ViewerGames.Handler
{
    internal sealed class EnlistPlayerCommandHandler : IRequestHandler<EnlistPlayerCommand>
    {
        private readonly IStreamCommunityContext streamCommunityContext;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IMediator mediator;
        private readonly ILogger<EnlistPlayerCommandHandler> logger;

        public EnlistPlayerCommandHandler(
            IStreamCommunityContext streamCommunityContext,
            IDateTimeProvider dateTimeProvider,
            IMediator mediator,
            ILogger<EnlistPlayerCommandHandler> logger)
        {
            this.streamCommunityContext = streamCommunityContext;
            this.dateTimeProvider = dateTimeProvider;
            this.mediator = mediator;
            this.logger = logger;
        }

        public async Task<Unit> Handle(EnlistPlayerCommand request, CancellationToken cancellationToken)
        {
            // check if an open or active enlistment exists
            if (streamCommunityContext.Enlistments.Any(
                x => x.UserName == request.UserName
                     && (x.State == EnlistmentState.Active || x.State == EnlistmentState.Open)))
            {
                logger.LogInformation("User {UserName} has already an open or active enlistment", request.UserName);
                await mediator.Publish(new PlayerEnlistmentFailed(request.UserName, "Bereits vorgemerkt"), cancellationToken);
                return Unit.Value;
            }

            // get the current max sorting number and apply
            var openEnlistments = streamCommunityContext.Enlistments
                .Where(x =>
                    x.State == EnlistmentState.Open &&
                    x.SortingNo.HasValue);
            var currentMax = openEnlistments.Any()
                ? openEnlistments?
                    .Max(x => x.SortingNo.Value) ?? 0
                : 0;

            var enlistment = new Enlistment(request.UserName, dateTimeProvider.UtcNow, currentMax + 1);
            streamCommunityContext.Enlistments.Add(enlistment);
            await streamCommunityContext.SaveChangesAsync(cancellationToken);


            await mediator.Publish(new PlayerEnlisted(request.UserName), cancellationToken);

            return Unit.Value;
        }
    }
}