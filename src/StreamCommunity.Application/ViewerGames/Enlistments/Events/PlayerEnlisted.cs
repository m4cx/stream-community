using System;
using MediatR;

namespace StreamCommunity.Application.ViewerGames.Enlistments.Events
{
    public sealed class PlayerEnlisted : INotification
    {
        public PlayerEnlisted(string userName)
        {
            UserName = userName ?? throw new AggregateException(nameof(userName));
        }

        public string UserName { get; }

        public string Type => EventNames.PlayerEnlisted;
    }
}