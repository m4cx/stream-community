using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TwitchCommunity.Application.Enlistments;

namespace TwitchCommunity.Application.Persistence
{
    public interface ITwitchCommunityContext
    {
        DbSet<Enlistment> Enlistments { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}