using MediatR;

namespace StreamCommunity.Application.ViewerGames.Enlistments.Events
{
    public sealed class PlayerEnlisted : INotification
    {
        public PlayerEnlisted(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; }

        public string Type => EventNames.PlayerEnlisted;
    }
}