using Microsoft.EntityFrameworkCore;
using StreamCommunity.Application.Persistence;
using StreamCommunity.Domain;

namespace StreamCommunity.Persistence
{
    public sealed class StreamCommunityDbContext : DbContext, IStreamCommunityContext
    {
        public StreamCommunityDbContext(DbContextOptions<StreamCommunityDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<Enlistment> Enlistments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}