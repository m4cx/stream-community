using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StreamCommunity.Application.Persistence;
using StreamCommunity.Domain;
using StreamCommunity.Persistence;

namespace StreamCommunity.Application.Tests;

public abstract class MockDbTestBase
{
    protected IStreamCommunityContext DbContext = null!;

    [SetUp]
    public void Setup()
    {
        var dbContextOptions = new DbContextOptionsBuilder<StreamCommunityDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        DbContext = new StreamCommunityDbContext(dbContextOptions);
    }

    protected async Task AddEnlistmentsAsync(IEnumerable<Enlistment> entities, CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
        {
            DbContext.Enlistments.Add(entity);
        }

        await DbContext.SaveChangesAsync(cancellationToken);
    }
}