using System;
using TwitchCommunity.Domain;

namespace StreamCommunity.Api.ViewerGames
{
    public class EnlistmentModel
    {
        public int Id { get; set; }

        /// <summary>
        /// Gets the username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets the timestamp the enlistment was created
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets the state of the enlistment
        /// </summary>
        public EnlistmentState State { get; set; }
    }
}
