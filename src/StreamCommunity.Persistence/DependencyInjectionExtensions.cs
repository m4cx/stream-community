using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StreamCommunity.Application.Persistence;
using StreamCommunity.Persistence.Configuration;

namespace StreamCommunity.Persistence
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddTwitchCommunityPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddOptions();

            var persistenceConfiguration = new PersistenceConfiguration();
            var configurationSection = configuration.GetSection(PersistenceConfiguration.ConfigurationSectionName);
            configurationSection.Bind(persistenceConfiguration);
            services.Configure<PersistenceConfiguration>(configurationSection);

            services.AddDbContext<StreamCommunityDbContext>(options =>
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

            services.AddScoped<IStreamCommunityContext>(options =>
                options.GetRequiredService<StreamCommunityDbContext>());

            return services;
        }
    }
}