using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchCommunity.Connector
{
    internal sealed class TwitchConnectorHostedService : BackgroundService, IHostedService
    {
        private TwitchConnector connector;
        private readonly IServiceScopeFactory serviceScopeFactory;

        public TwitchConnectorHostedService(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = serviceScopeFactory.CreateScope();
            connector = scope.ServiceProvider.GetRequiredService<TwitchConnector>();

            connector.Connect();

            while(!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(500);
            }

            connector.Disconnect();
        }
    }
}
