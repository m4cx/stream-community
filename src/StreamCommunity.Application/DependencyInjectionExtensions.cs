using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StreamCommunity.Application.ChatMessages.Configuration;
using StreamCommunity.Application.Common;

namespace StreamCommunity.Application
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddTwitchCommunityApplication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddOptions();

            services.AddChatMessagesConfiguration(configuration);

            services.AddMediatR(typeof(DependencyInjectionExtensions).Assembly);

            services.AddTransient<IDateTimeProvider, DefaultDateTimeProvider>();
            return services;
        }

        private static ChatMessagesConfiguration AddChatMessagesConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var persistenceConfiguration = new ChatMessagesConfiguration();
            var configurationSection = configuration.GetSection(ChatMessagesConfiguration.ConfigurationSectionName);
            configurationSection.Bind(persistenceConfiguration);
            services.Configure<ChatMessagesConfiguration>(configurationSection);
            return persistenceConfiguration;
        }
    }
}