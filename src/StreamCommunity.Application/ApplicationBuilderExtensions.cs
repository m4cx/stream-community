using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StreamCommunity.Application.ChatMessages.Configuration;
using StreamCommunity.Application.Persistence;
using StreamCommunity.Domain;

namespace StreamCommunity.Application;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder ApplyChatMessageTemplatesFromConfiguration(this IApplicationBuilder app)
    {
        var chatMessageConfiguration =
            app.ApplicationServices.GetRequiredService<IOptions<ChatMessagesConfiguration>>();
        var templates = chatMessageConfiguration?.Value?.Templates;
        if (templates == null)
        {
            return app;
        }

        using var dbContext = app.ApplicationServices
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<IStreamCommunityContext>();

        foreach (var template in templates)
        {
            var templateInstance =
                dbContext.ChatMessageTemplates.SingleOrDefault(x => x.Identifier == template.Identifier);
            if (templateInstance != null)
            {
                templateInstance.Message = template.MessageTemplate;
            }
            else
            {
                dbContext.ChatMessageTemplates.Add(new ChatMessageTemplate(
                    template.Identifier,
                    template.MessageTemplate));
            }
        }

        dbContext.SaveChangesAsync().Wait();

        return app;
    }
}