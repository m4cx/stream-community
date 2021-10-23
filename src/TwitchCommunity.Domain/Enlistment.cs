using System;

namespace TwitchCommunity.Domain
{
    public class Enlistment
    {
        private Enlistment() { }

        /// <summary>
        /// Creates a new <see cref="Enlistment"/> instance
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="timestamp"></param>
        public Enlistment(string userName, DateTime timestamp)
        {
            State = EnlistmentState.Open;
            UserName = userName;
            Timestamp = timestamp;
        }

        public int Id { get; private set; }

        /// <summary>
        /// Gets the username
        /// </summary>
        public string UserName { get; }

        /// <summary>
        /// Gets the timestamp the enlistment was created
        /// </summary>
        public DateTime Timestamp { get; }

        /// <summary>
        /// Gets the state of the enlistment
        /// </summary>
        public EnlistmentState State { get; private set; }

        /// <summary>
        /// Draw a player to be active  
        /// </summary>
        public void Draw()
        {
            State = EnlistmentState.Active;
        }

        /// <summary>
        /// Close the enlistment session
        /// </summary>
        public void Close()
        {
            State = EnlistmentState.Closed;
        }
    }
}