using System.Collections.Generic;
using TwitchCommunity.Domain;

namespace TwitchCommunity.Application.Enlistments
{
    public class GetEnlistmentsResponse
    {
        public GetEnlistmentsResponse(IEnumerable<Enlistment> enlistments)
        {
            Enlistments = enlistments;
        }

        public IEnumerable<Enlistment> Enlistments { get; }
    }
}