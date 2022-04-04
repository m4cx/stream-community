using System;
using MediatR;

namespace StreamCommunity.Application.ViewerGames.Events
{
    public class PlayerDrawn : EventBase, INotification
    {
        public PlayerDrawn(string userName)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        }

        public string UserName { get; }

        public override string Type => EventNames.PlayerDrawn;
    }
}