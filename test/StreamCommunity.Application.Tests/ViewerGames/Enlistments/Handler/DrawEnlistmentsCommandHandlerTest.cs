using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using NUnit.Framework;
using StreamCommunity.Application.ViewerGames;
using StreamCommunity.Application.ViewerGames.Events;
using StreamCommunity.Application.ViewerGames.Handler;
using StreamCommunity.Domain;

namespace StreamCommunity.Application.Tests.ViewerGames.Enlistments.Handler;

public class DrawEnlistmentsCommandHandlerTest : MockDbTestBase
{
    private DrawEnlistmentsCommandHandler instance = null!;
    private Mock<IMediator> mediatorMock = null!;

    [SetUp]
    public void SetUp()
    {
        mediatorMock = new Mock<IMediator>();

        instance = new DrawEnlistmentsCommandHandler(DbContext!, mediatorMock.Object);
    }

    [Test]
    public async Task Handle_OpenEnlistment_StateIsActiveAsync()
    {
        var enlistment = new Enlistment("testUser", DateTime.Now, 1);
        DbContext.Enlistments.Add(enlistment);
        await DbContext.SaveChangesAsync();

        await instance.Handle(new DrawEnlistmentsCommand(new[] { enlistment.Id }), CancellationToken.None);

        Assert.AreEqual(EnlistmentState.Active, enlistment.State);
    }

    [Test]
    public async Task Handle_OpenEnlistment_EventIsSendAsync()
    {
        var enlistment = new Enlistment("testUser", DateTime.Now, 1);
        DbContext.Enlistments.Add(enlistment);
        await DbContext.SaveChangesAsync();

        await instance.Handle(new DrawEnlistmentsCommand(new[] { enlistment.Id }), CancellationToken.None);

        mediatorMock.Verify(
            x => x.Publish(
                It.Is<PlayerDrawn>(pd => pd.UserName == "testUser"),
                It.IsAny<CancellationToken>()), 
            Times.Once);
    }
}