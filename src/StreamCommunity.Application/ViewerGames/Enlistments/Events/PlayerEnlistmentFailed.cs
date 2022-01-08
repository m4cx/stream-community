using System;
using JetBrains.Annotations;
using MediatR;

namespace StreamCommunity.Application.ViewerGames.Enlistments.Events;

/// <summary>
/// This event is published, if a player requesting an enlistment for a viewer game fails.
/// </summary>
public class PlayerEnlistmentFailed : INotification
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerEnlistmentFailed"/> class.
    /// </summary>
    /// <param name="userName">The name of the viewer.</param>
    /// <param name="reason">An additional reason (optional).</param>
    /// <exception cref="ArgumentNullException">If null is provided for the <param name="userName"></param>.</exception>
    public PlayerEnlistmentFailed(string userName, string reason = null)
    {
        UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        Reason = reason;
    }

    /// <summary>
    /// Gets the user name of the viewer requesting an enlistment.
    /// </summary>
    public string UserName { get; }

    /// <summary>
    /// Gets an additional reason why the enlistment failed. (optional - can be null)
    /// </summary>
    [CanBeNull]
    public string Reason { get; }
}