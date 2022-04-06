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

[TestFixture]
public class WithdrawPlayerCommandHandlerTest : MockDbTestBase
{
    private Mock<IMediator> mediatorMock = null!;
    private WithdrawPlayerCommandHandler instance = null!;

    [SetUp]
    public Task InitializeTest()
    {
        mediatorMock = new Mock<IMediator>();
        instance = new WithdrawPlayerCommandHandler(DbContext, mediatorMock.Object);

        return Task.CompletedTask;
    }

    [Test]
    public async Task Handle_WithNotKnowUserName_SendsFailedNotification()
    {
        const string userName = "testUser";
        await instance.Handle(new WithdrawPlayerCommand(userName), CancellationToken.None);

        mediatorMock.Verify(
            x => x.Publish(
                It.Is<PlayerWithdrawelFailed>(n =>
                    n.UserName == userName &&
                    n.Reason == PlayerWithdrawelFailedReason.PlayerNotFound),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Test]
    public async Task Handle_WithKnowUserName_SendsSuccessNotification()
    {
        const string userName = "testUser";
        DbContext.Enlistments.Add(new Enlistment(userName, DateTime.UtcNow, 1));
        await DbContext.SaveChangesAsync();

        await instance.Handle(new WithdrawPlayerCommand(userName), CancellationToken.None);

        mediatorMock.Verify(
            x => x.Publish(
                It.Is<PlayerWithdrawn>(n =>
                    n.UserName == userName),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Test]
    public async Task Handle_WithKnowUserName_StateIsChangedToWithdrawn()
    {
        const string userName = "testUser";
        var enlistment = new Enlistment(userName, DateTime.UtcNow, 1);
        DbContext.Enlistments.Add(enlistment);
        await DbContext.SaveChangesAsync();

        await instance.Handle(new WithdrawPlayerCommand(userName), CancellationToken.None);

        Assert.AreEqual(EnlistmentState.Withdrawn, enlistment.State);
    }
}