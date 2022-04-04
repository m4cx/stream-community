using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StreamCommunity.Application.Persistence;
using StreamCommunity.Domain;

namespace StreamCommunity.Application.ViewerGames.EventHandler;

internal abstract class ChatMessageEventHandlerBase
{
    private readonly IStreamCommunityContext communityContext;

    protected ChatMessageEventHandlerBase(IStreamCommunityContext communityContext)
    {
        this.communityContext = communityContext;
    }

    protected abstract string ChatMessageTemplateIdentifier { get; }

    protected async Task<ChatMessageTemplate> GetChatMessageTemplateAsync(CancellationToken cancellationToken)
    {
        return await communityContext.ChatMessageTemplates
            .SingleOrDefaultAsync(
                x => x.Identifier == ChatMessageTemplateIdentifier,
                cancellationToken);
    }
}