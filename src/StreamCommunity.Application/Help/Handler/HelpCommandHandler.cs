using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StreamCommunity.Application.ChatMessages;
using StreamCommunity.Application.Common;
using StreamCommunity.Application.Persistence;

namespace StreamCommunity.Application.Help.Handler;

[UsedImplicitly]
internal sealed class HelpCommandHandler : IRequestHandler<HelpCommand>
{
    private readonly IStreamCommunityContext streamCommunityContext;
    private readonly IChatMessaging chatMessaging;

    public HelpCommandHandler(IStreamCommunityContext streamCommunityContext, IChatMessaging chatMessaging)
    {
        this.streamCommunityContext = streamCommunityContext;
        this.chatMessaging = chatMessaging;
    }

    public async Task<Unit> Handle(HelpCommand request, CancellationToken cancellationToken)
    {
        var messageTemplate = await streamCommunityContext.ChatMessageTemplates
            .SingleAsync(
                x => x.Identifier == ChatMessageTemplateIdentifiers.Help,
                cancellationToken);

        await chatMessaging.SendMessageAsync(messageTemplate.Message);
        return Unit.Value;
    }
}