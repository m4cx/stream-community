using System;

namespace TwitchCommunity.Application.Enlistments
{
    public class Enlistment
    {
        private Enlistment() { }

        public Enlistment(string userName, DateTime timestamp)
        {
            State = EnlistmentState.Open;
            UserName = userName;
            Timestamp = timestamp;
        }

        public int Id { get; private set; }

        public string UserName { get; }

        public DateTime Timestamp { get; }

        public EnlistmentState State { get; set; }
    }
}