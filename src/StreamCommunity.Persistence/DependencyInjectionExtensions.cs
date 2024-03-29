﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StreamCommunity.Application.Persistence;
using StreamCommunity.Persistence.Configuration;

namespace StreamCommunity.Persistence
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddStreamCommunityPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddOptions();

            var persistenceConfiguration = AddPersistenceConfiguration(services, configuration);

            services.AddDbContext<StreamCommunityDbContext>(options =>
            {
                switch (persistenceConfiguration.ProviderName)
                {
                    case PersistenceProviderNames.Sqlite:
                        options.UseSqlite(
                            persistenceConfiguration.ConnectionString,
                            sqliteOptions => sqliteOptions.MigrationsAssembly("StreamCommunity.Persistence.Migrations"));
                        break;
                    case PersistenceProviderNames.InMemory:
                        options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                        break;
                }

#if DEBUG
                options.EnableSensitiveDataLogging().EnableDetailedErrors();
#endif
            });

            services.AddTransient<IStreamCommunityContext>(provider =>
                provider.GetRequiredService<StreamCommunityDbContext>());

            return services;
        }

        private static PersistenceConfiguration AddPersistenceConfiguration(
            IServiceCollection services,
            IConfiguration configuration)
        {
            var persistenceConfiguration = new PersistenceConfiguration();
            var configurationSection = configuration.GetSection(PersistenceConfiguration.ConfigurationSectionName);
            configurationSection.Bind(persistenceConfiguration);
            services.Configure<PersistenceConfiguration>(configurationSection);
            return persistenceConfiguration;
        }
    }
}