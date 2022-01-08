using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using StreamCommunity.Application.ViewerGames.Enlistments;
using StreamCommunity.Application.ViewerGames.Enlistments.Handler;
using StreamCommunity.Domain;

namespace StreamCommunity.Application.Tests.ViewerGames.Enlistments.Handler;

public class GetEnlistmentsRequestHandlerTest : MockDbTestBase
{
    private GetEnlistmentsRequestHandler instance = null!;

    [SetUp]
    public void SetUp()
    {
        instance = new GetEnlistmentsRequestHandler(DbContext);
    }

    [Test]
    public async Task Handle_WithNoEnlistments_ReturnsEmptyListAsync()
    {
        var response = await instance.Handle(new GetEnlistmentsRequest(), CancellationToken.None);

        Assert.IsEmpty(response.Enlistments);
    }

    [Test]
    public async Task Handle_WithMultipleEnlistmentsWithOpenState_ReturnsAllOpenAsync()
    {
        var enlistment1 = new Enlistment("testUser1", DateTime.Now);
        var enlistment2 = new Enlistment("testUser2", DateTime.Now);
        await AddEnlistmentsAsync(new[]
        {
            enlistment1,
            enlistment2
        });

        var response = await instance.Handle(new GetEnlistmentsRequest(), CancellationToken.None);

        var collection = response.Enlistments.ToList();
        Assert.AreEqual(2, collection.Count);
        Assert.Contains(enlistment1, collection);
        Assert.Contains(enlistment2, collection);
    }

    [Test]
    public async Task Handle_WithClosedRequestAndRequestClosed_ReturnsTheClosedEnlistmentAsync()
    {
        var enlistment1 = new Enlistment("testUser1", DateTime.Now);
        enlistment1.Draw();
        enlistment1.Close();

        await AddEnlistmentsAsync(new[]
        {
            enlistment1,
        });

        var response = await instance.Handle(
            new GetEnlistmentsRequest(EnlistmentState.Closed), 
            CancellationToken.None);

        var collection = response.Enlistments.ToList();
        Assert.AreEqual(1, collection.Count);
        Assert.Contains(enlistment1, collection);
    }
}
