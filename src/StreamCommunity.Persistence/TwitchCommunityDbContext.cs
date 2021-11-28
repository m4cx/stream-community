using Microsoft.EntityFrameworkCore;
using StreamCommunity.Application.Persistence;
using StreamCommunity.Domain;

namespace StreamCommunity.Persistence
{
    public sealed class TwitchCommunityDbContext : DbContext, ITwitchCommunityContext
    {
        public TwitchCommunityDbContext(DbContextOptions<TwitchCommunityDbContext> dbContextOptions)
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