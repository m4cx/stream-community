using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using StreamCommunity.Application.Common;
using StreamCommunity.Application.ViewerGames;
using StreamCommunity.Application.ViewerGames.Events;
using StreamCommunity.Application.ViewerGames.Handler;
using StreamCommunity.Domain;

namespace StreamCommunity.Application.Tests.ViewerGames.Enlistments.Handler;

public class EnlistPlayerCommandHandlerTest : MockDbTestBase
{
    private const string UserName = "testUser";

    private Mock<IDateTimeProvider> dateTimeProviderMock = null!;
    private Mock<IMediator> mediatorMock = null!;
    private Mock<ILogger<EnlistPlayerCommandHandler>> loggerMock = null!;
    private EnlistPlayerCommandHandler instance = null!;

    [SetUp]
    public void SetUp()
    {
        dateTimeProviderMock = new Mock<IDateTimeProvider>();
        mediatorMock = new Mock<IMediator>();
        loggerMock = new Mock<ILogger<EnlistPlayerCommandHandler>>();

        instance = new EnlistPlayerCommandHandler(DbContext, dateTimeProviderMock.Object, mediatorMock.Object,
            loggerMock.Object);
    }

    [Test]
    public async Task Handle_WithNewEnlistment_DatabaseEntryIsCreatedAsync()
    {
        await instance.Handle(new EnlistPlayerCommand(UserName), CancellationToken.None);

        var savedEnlistment = DbContext.Enlistments.SingleOrDefault(x => x.UserName == UserName);
        Assert.IsNotNull(savedEnlistment);
    }

    [Test]
    public async Task Handle_WithNewEnlistment_PlayerEnlistedIsSentAsync()
    {
        await instance.Handle(new EnlistPlayerCommand(UserName), CancellationToken.None);

        mediatorMock.Verify(
            x => x.Publish(
                It.Is<PlayerEnlisted>(pe => pe.UserName == UserName),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Test]
    public async Task Handle_WithExistingEnlistment_PlayerEnlistmentFailedIsSentAsync()
    {
        DbContext.Enlistments.Add(new Enlistment(UserName, DateTime.Now, 1));
        await DbContext.SaveChangesAsync();

        await instance.Handle(new EnlistPlayerCommand(UserName), CancellationToken.None);

        mediatorMock.Verify(
            x => x.Publish(
                It.Is<PlayerEnlistmentFailed>(pe => pe.UserName == UserName),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Test]
    public async Task Handle_WithExistingEnlistment_LogoutputIsLoggedAsync()
    {
        var enlistment = new Enlistment(UserName, DateTime.Now, 1);
        DbContext.Enlistments.Add(enlistment);
        await DbContext.SaveChangesAsync();

        await instance.Handle(new EnlistPlayerCommand(UserName), CancellationToken.None);

        loggerMock.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>) It.IsAny<object>()),
            Times.Once);
    }
}