using System;

namespace TwitchCommunity.Application.Enlistments
{
    public class Enlistment
    {
        private Enlistment() { }

        public Enlistment(string userName, DateTime timestamp)
        {
            UserName = userName;
            Timestamp = timestamp;
        }

        public int Id { get; private set; }

        public string UserName { get; }

        public DateTime Timestamp { get; }
    }
}