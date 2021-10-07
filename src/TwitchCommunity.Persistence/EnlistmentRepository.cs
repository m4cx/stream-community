using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TwitchCommunity.Application.Enlistments;

namespace TwitchCommunity.Persistence
{
    internal sealed class EnlistmentRepository : IEnlistmentRepository
    {
        private readonly TwitchCommunityDbContext dbContext;

        public EnlistmentRepository(TwitchCommunityDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(Enlistment enlistment, CancellationToken cancellationToken = default)
        {
            dbContext.Add(enlistment);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Enlistment>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.Enlistments
                .OrderBy(x => x.Timestamp)
                .ToListAsync(cancellationToken);
        }
    }
}