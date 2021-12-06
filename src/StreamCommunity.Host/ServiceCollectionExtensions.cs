using Microsoft.Extensions.DependencyInjection;

namespace StreamCommunity.Host
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/> instance handling the dependency injection framework.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add web application services to dependency injection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>returns the service collection.</returns>
        public static IServiceCollection AddWebApplication(this IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSignalR();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });

            return services;
        }
    }
}