using Microsoft.EntityFrameworkCore;
using StreamCommunity.Application.Persistence;
using StreamCommunity.Domain;

namespace StreamCommunity.Persistence
{
    internal sealed class StreamCommunityDbContext : DbContext, IStreamCommunityContext
    {
        public StreamCommunityDbContext(DbContextOptions<StreamCommunityDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<Enlistment> Enlistments { get; set; }

        public DbSet<ChatMessageTemplate> ChatMessageTemplates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}