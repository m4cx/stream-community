using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StreamCommunity.Domain;

namespace StreamCommunity.Application.Persistence
{
    public interface ITwitchCommunityContext
    {
        DbSet<Enlistment> Enlistments { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}