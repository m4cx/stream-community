﻿using System;
using StreamCommunity.Domain.Exceptions;

namespace StreamCommunity.Domain
{
    public class Enlistment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Enlistment"/> class.
        /// </summary>
        /// <param name="userName">name of the user.</param>
        /// <param name="timestamp">creation timestamp.</param>
        public Enlistment(string userName, DateTime timestamp)
        {
            State = EnlistmentState.Open;
            UserName = userName;
            Timestamp = timestamp;
        }

        /// <summary>
        /// Gets the unique identifier of the <see cref="Enlistment"/> instance.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the username.
        /// </summary>
        public string UserName { get; }

        /// <summary>
        /// Gets the timestamp the enlistment was created.
        /// </summary>
        public DateTime Timestamp { get; }

        /// <summary>
        /// Gets the state of the enlistment.
        /// </summary>
        public EnlistmentState State { get; private set; }

        /// <summary>
        /// Draw a player to be active.
        /// </summary>
        public void Draw()
        {
            if (State != EnlistmentState.Open)
            {
                throw new EnlistmentException(
                    $"Enlistment needs to be in state 'Active' in order to be closed. Current State: {Enum.GetName(State)}");
            }

            State = EnlistmentState.Active;
        }

        /// <summary>
        /// Close the enlistment session.
        /// </summary>
        public void Close()
        {
            if (State != EnlistmentState.Active)
            {
                throw new EnlistmentException(
                    $"Enlistment needs to be in state 'Active' in order to be closed. Current State: {Enum.GetName(State)}");
            }

            State = EnlistmentState.Closed;
        }

        /// <summary>
        /// Withdraw from enlistment.
        /// </summary>
        /// <exception cref="EnlistmentException">When <see cref="State"/> is not <see cref="EnlistmentState.Open"/>.</exception>
        public void Withdraw()
        {
            if (State != EnlistmentState.Open)
            {
                throw new EnlistmentException(
                    $"Enlistment needs to be in state 'Open' in order to be withdrawn. Current State: {Enum.GetName(State)}");
            }

            State = EnlistmentState.Withdrawn;
        }
    }
}