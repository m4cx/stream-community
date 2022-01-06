using System;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StreamCommunity.Application.Persistence;
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
}