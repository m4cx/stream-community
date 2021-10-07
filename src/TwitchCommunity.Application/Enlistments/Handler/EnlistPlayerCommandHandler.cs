using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TwitchCommunity.Application.Common;

namespace TwitchCommunity.Application.Enlistments.Handler
{
    internal sealed class EnlistPlayerCommandHandler : IRequestHandler<EnlistPlayerCommand>
    {
        private readonly IEnlistmentRepository enlistmentRepository;
        private readonly IDateTimeProvider dateTimeProvider;

        public EnlistPlayerCommandHandler(IEnlistmentRepository enlistmentRepository, IDateTimeProvider dateTimeProvider)
        {
            this.enlistmentRepository = enlistmentRepository;
            this.dateTimeProvider = dateTimeProvider;
        }

        public async Task<Unit> Handle(EnlistPlayerCommand request, CancellationToken cancellationToken)
        {
            var enlistment = new Enlistment(request.UserName, dateTimeProvider.UtcNow);
            await enlistmentRepository.AddAsync(enlistment);
            return Unit.Value;
        }
    }
}
