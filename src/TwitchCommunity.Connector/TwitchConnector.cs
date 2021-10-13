using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
        private readonly ILogger<TwitchConnector> logger;

        public TwitchConnector(
            IOptions<TwitchConnectorConfiguration> connectorConfiguration, 
            ILogger<TwitchConnector> logger)
        {
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

        private void InitializeEvents()
        {
            twitchClient.OnConnected += TwitchClient_OnConnected;
            twitchClient.OnMessageReceived += TwitchClient_OnMessageReceived;
        }

        private void TwitchClient_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            logger.LogInformation("Message Received: {e}", e.ChatMessage.Message);
            if (e.ChatMessage.Message.StartsWith("!m4cx"))
                twitchClient.SendMessage(e.ChatMessage.Channel, "Ich bin immer da und sehe alles!");

        }

        private void TwitchClient_OnConnected(object sender, OnConnectedArgs e)
        {
            logger.LogInformation("Connected to {channel}", e.AutoJoinChannel);
        }
    }
}
