using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TwitchCommunity.Connector.Configuration;

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
            services.AddSingleton<TwitchConnector>();

            return services;
        }
    }
}
