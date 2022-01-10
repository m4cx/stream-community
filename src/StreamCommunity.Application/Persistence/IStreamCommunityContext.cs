using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using StreamCommunity.Domain;

namespace StreamCommunity.Application.Persistence
{
    public interface IStreamCommunityContext
    {
        DatabaseFacade Database { get; }

        DbSet<Enlistment> Enlistments { get; set; }

        DbSet<ChatMessageTemplate> ChatMessageTemplates { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}