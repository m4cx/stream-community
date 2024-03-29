﻿using System;

namespace StreamCommunity.Api.ViewerGames
{
    public class EnlistmentModel
    {
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the timestamp the enlistment was created.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the state of the enlistment.
        /// </summary>
        public EnlistmentState State { get; set; }
    }
}
