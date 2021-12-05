using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StreamCommunity.Application.Common;

namespace StreamCommunity.Application
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddTwitchCommunityApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(DependencyInjectionExtensions).Assembly);

            services.AddTransient<IDateTimeProvider, DefaultDateTimeProvider>();

            return services;
        }
    }
}