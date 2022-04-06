using System;
using JetBrains.Annotations;
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
        /// <param name="sortingNo">number for sorting enlistments.</param>
        public Enlistment(string userName, DateTime timestamp, int sortingNo)
        {
            State = EnlistmentState.Open;
            UserName = userName;
            Timestamp = timestamp;
            SortingNo = sortingNo;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Enlistment"/> class.
        /// </summary>
        [UsedImplicitly]
        private Enlistment()
        {
        }

        /// <summary>
        /// Gets the unique identifier of the <see cref="Enlistment"/> instance.
        /// </summary>
        [UsedImplicitly]
        public int Id { get; }

        /// <summary>
        /// Gets the username.
        /// </summary>
        public string UserName { get; }

        /// <summary>
        /// Gets the timestamp the enlistment was created.
        /// </summary>
        public DateTime Timestamp { get; }

        /// <summary>
        /// Gets the number of order for sorting in the waiting list
        /// </summary>
        /// <remarks>
        /// This property will only be filled when in State <see cref="EnlistmentState.Open"/>.
        /// </remarks>
        public int? SortingNo { get; set; }

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

            SortingNo = null;
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

            SortingNo = null;
            State = EnlistmentState.Withdrawn;
        }
    }
}