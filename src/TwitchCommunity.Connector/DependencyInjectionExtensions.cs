using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwitchCommunity.Application;
using TwitchCommunity.Connector.Configuration;
using TwitchCommunity.Persistence;

namespace TwitchCommunity.Connector
{
    public static class DependencyInjectionExtensions
    {
        private const string ConfigurationSectionName = "TwitchCommunity:Connector";

        public static IServiceCollection AddTwitchCommunityConnector(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var configurationSection = configuration.GetSection(ConfigurationSectionName);
            services.Configure<TwitchConnectorConfiguration>(configurationSection);
            services.AddScoped<TwitchConnector>();
            services.AddHostedService<TwitchConnectorHostedService>();

            services.AddTwitchCommunityApplication(configuration);
            services.AddTwitchCommunityPersistence(configuration);

            return services;
        }
    }
}
