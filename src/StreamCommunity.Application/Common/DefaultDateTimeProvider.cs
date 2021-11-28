using System;

namespace StreamCommunity.Application.Common
{
    /// <summary>
    /// The default implementation of <see cref="IDateTimeProvider"/> using <see cref="DateTime"/> class.
    /// </summary>
    internal sealed class DefaultDateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
