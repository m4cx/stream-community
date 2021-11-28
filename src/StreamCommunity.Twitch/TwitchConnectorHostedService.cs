using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace StreamCommunity.Twitch
{
    internal sealed class TwitchConnectorHostedService : BackgroundService, IHostedService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private TwitchConnector connector;

        public TwitchConnectorHostedService(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = serviceScopeFactory.CreateScope();
            connector = scope.ServiceProvider.GetRequiredService<TwitchConnector>();

            connector.Connect();

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(500);
            }

            connector.Disconnect();
        }
    }
}