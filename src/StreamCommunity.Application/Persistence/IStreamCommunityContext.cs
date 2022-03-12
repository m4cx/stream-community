using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using StreamCommunity.Domain;

namespace StreamCommunity.Application.Persistence
{
    public interface IStreamCommunityContext : IDisposable
    {
        DatabaseFacade Database { get; }

        DbSet<Enlistment> Enlistments { get; set; }

        DbSet<ChatMessageTemplate> ChatMessageTemplates { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}