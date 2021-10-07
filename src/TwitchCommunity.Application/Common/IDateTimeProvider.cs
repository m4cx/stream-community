using System;

namespace TwitchCommunity.Application.Common
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}