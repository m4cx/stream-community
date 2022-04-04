using System;
using System.Collections.Generic;
using StreamCommunity.Domain;

namespace StreamCommunity.Application.ViewerGames
{
    public class GetEnlistmentsResponse
    {
        public GetEnlistmentsResponse(IEnumerable<Enlistment> enlistments)
        {
            Enlistments = enlistments ?? throw new ArgumentNullException(nameof(enlistments));
        }

        public IEnumerable<Enlistment> Enlistments { get; }
    }
}