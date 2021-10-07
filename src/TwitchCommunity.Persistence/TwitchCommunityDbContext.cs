using Microsoft.EntityFrameworkCore;
using TwitchCommunity.Application.Enlistments;

namespace TwitchCommunity.Persistence
{
    public sealed class TwitchCommunityDbContext : DbContext
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
