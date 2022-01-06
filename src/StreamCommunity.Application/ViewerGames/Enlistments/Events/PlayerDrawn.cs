using System;
using MediatR;

namespace StreamCommunity.Application.ViewerGames.Enlistments.Events
{
    public class PlayerDrawn : INotification
    {
        public PlayerDrawn(string userName)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        }

        public string UserName { get; }

        public string Type => EventNames.PlayerDrawn;
    }
}