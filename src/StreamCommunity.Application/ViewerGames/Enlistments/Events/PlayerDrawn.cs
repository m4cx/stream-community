using MediatR;

namespace StreamCommunity.Application.ViewerGames.Enlistments.Events
{
    public class PlayerDrawn : INotification
    {
        public string UserName { get; }

        public PlayerDrawn(string userName)
        {
            UserName = userName;
        }

        public string Type => EventNames.PlayerDrawn;
    }
}