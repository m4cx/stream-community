using System;

namespace StreamCommunity.Application.Common
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}