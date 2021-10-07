using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchCommunity.Application.Enlistments
{
    public sealed class GetEnlistmentsRequest : IRequest<GetEnlistmentsResponse>
    {
    }
}
