using MediatR;

namespace StreamCommunity.Application.ViewerGames.Enlistments
{
    public sealed class PlayerEnlisted : INotification
    {
        public PlayerEnlisted(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; }

        public string Type => "PLAYER_ENLISTED";
    }
}