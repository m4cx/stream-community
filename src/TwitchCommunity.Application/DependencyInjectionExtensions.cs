using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwitchCommunity.Application.Common;

namespace TwitchCommunity.Application
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddTwitchCommunityApplication(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddMediatR(typeof(DependencyInjectionExtensions).Assembly);

            services.AddTransient<IDateTimeProvider, DefaultDateTimeProvider>();

            return services;
        }
    }
}
