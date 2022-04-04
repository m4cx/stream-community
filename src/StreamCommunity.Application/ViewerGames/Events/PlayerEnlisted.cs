using System;
using MediatR;

namespace StreamCommunity.Application.ViewerGames.Events
{
    public sealed class PlayerEnlisted : EventBase, INotification
    {
        public PlayerEnlisted(string userName)
        {
            UserName = userName ?? throw new AggregateException(nameof(userName));
        }

        public string UserName { get; }

        public override string Type => EventNames.PlayerEnlisted;
    }
}