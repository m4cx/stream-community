using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TwitchCommunity.Application.Enlistments;
using TwitchCommunity.Application.Persistence;
using TwitchCommunity.Persistence.Configuration;

namespace TwitchCommunity.Persistence
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddTwitchCommunityPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();

            var persistenceConfiguration = new PersistenceConfiguration();
            var configurationSection = configuration.GetSection(PersistenceConfiguration.ConfigurationSectionName);
            configurationSection.Bind(persistenceConfiguration);
            services.Configure<PersistenceConfiguration>(configurationSection);

            services.AddDbContext<TwitchCommunityDbContext>(options =>
            {
                if (persistenceConfiguration.ProviderName == "sqlite")
                {
                    options.UseSqlite(persistenceConfiguration.ConnectionString);
                }
                else if (persistenceConfiguration.ProviderName == "in-memory")
                {
                    options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                }

#if DEBUG
                options.EnableSensitiveDataLogging().EnableDetailedErrors();
#endif
            });

            services.AddScoped<ITwitchCommunityContext>(options => options.GetRequiredService<TwitchCommunityDbContext>());

            return services;
        } 
    }
}
