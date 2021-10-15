using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using TwitchCommunity.Application.Enlistments;
using TwitchCommunity.Connector.Configuration;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace TwitchCommunity.Connector
{
    public sealed class TwitchConnector
    {
        private readonly TwitchClient twitchClient;
        private readonly IMediator mediator;
        private readonly ILogger<TwitchConnector> logger;

        public TwitchConnector(
            IOptions<TwitchConnectorConfiguration> connectorConfiguration,
            IMediator mediator,
            ILogger<TwitchConnector> logger)
        {
            this.mediator = mediator;
            this.logger = logger;

            var credentials = new ConnectionCredentials(
                connectorConfiguration.Value.UserName, 
                connectorConfiguration.Value.AccessToken);

            var clientOptions = new ClientOptions();
            var customClient = new WebSocketClient(clientOptions);

            twitchClient = new TwitchClient(customClient);
            twitchClient.Initialize(credentials, connectorConfiguration.Value.Channel);

            InitializeEvents();
        }

        public void Connect() => twitchClient.Connect();

        public void Disconnect() => twitchClient.Disconnect();

        private void InitializeEvents()
        {
            twitchClient.OnConnected += TwitchClient_OnConnected;
            twitchClient.OnMessageReceived += async (sender, e) => await OnMessageReceived(sender, e);
        }

        private async Task OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            const string EnlistmentKey = "!enlist";

            if (e.ChatMessage.Message.StartsWith(EnlistmentKey))
            {
                var command = new EnlistPlayerCommand(e.ChatMessage.Username);
                await mediator.Send(command);
            }
        }

        private void TwitchClient_OnConnected(object sender, OnConnectedArgs e)
        {
            logger.LogInformation("Connected to {channel}", e.AutoJoinChannel);
        }
    }
}
