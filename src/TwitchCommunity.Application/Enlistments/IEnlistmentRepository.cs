using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchCommunity.Application.Enlistments
{
    public interface IEnlistmentRepository
    {
        Task AddAsync(Enlistment enlistment, CancellationToken cancellationToken = default);
        Task<IEnumerable<Enlistment>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}