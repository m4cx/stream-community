using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StreamCommunity.Twitch.Configuration;
using StreamCommunity.Application;
using StreamCommunity.Persistence;

namespace StreamCommunity.Twitch
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
