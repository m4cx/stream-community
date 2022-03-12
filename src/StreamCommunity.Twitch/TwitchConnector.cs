#nullable enable
using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StreamCommunity.Application.Common;
using StreamCommunity.Application.Help;
using StreamCommunity.Application.ViewerGames.Enlistments;
using StreamCommunity.Twitch.Configuration;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace StreamCommunity.Twitch
{
    public sealed class TwitchConnector : IChatMessaging
    {
        private readonly TwitchClient twitchClient;
        private readonly IOptions<TwitchConnectorConfiguration> connectorConfiguration;
        private readonly IMediator mediator;
        private readonly ILogger<TwitchConnector> logger;

        public TwitchConnector(
            IOptions<TwitchConnectorConfiguration> connectorConfiguration,
            IMediator mediator,
            ILogger<TwitchConnector> logger)
        {
            this.connectorConfiguration = connectorConfiguration;
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

        public Task SendMessageAsync(string message)
        {
            twitchClient.SendMessage(connectorConfiguration.Value.Channel, message);
            return Task.CompletedTask;
        }

        private void InitializeEvents()
        {
            twitchClient.OnConnected += TwitchClient_OnConnected;
            twitchClient.OnChatCommandReceived += async (sender, e)
                => await OnChatCommandReceived(sender, e);
        }

        private async Task OnChatCommandReceived(object? sender, OnChatCommandReceivedArgs e)
        {
            var command = CreateCommand(e);
            if (command == null)
            {
                logger.LogInformation(
                    "Command {CommandText} was not recognized: {@Command}",
                    e.Command.CommandText,
                    e.Command);
                return;
            }

            await SendCommand(command);
        }

        private async Task SendCommand(IRequest command)
        {
            try
            {
                await mediator.Send(command);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to dispatch command: {@Command}", command);
            }
        }

        private IRequest? CreateCommand(OnChatCommandReceivedArgs e)
        {
            const string enlistmentKey = "enlist";
            const string HelpKey = "help";

            IRequest? command = null;
            switch (e.Command.CommandText)
            {
                case enlistmentKey:
                    command = new EnlistPlayerCommand(e.Command.ChatMessage.Username);
                    break;

                case HelpKey:
                    command = new HelpCommand();
                    break;

                default:
                    break;
            }

            return command;
        }

        private void TwitchClient_OnConnected(object? sender, OnConnectedArgs e)
        {
            logger.LogInformation("Connected to {Channel}", e.AutoJoinChannel);
        }
    }
}