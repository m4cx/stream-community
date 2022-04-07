using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using StreamCommunity.Application.ViewerGames;
using StreamCommunity.Application.ViewerGames.Handler;
using StreamCommunity.Domain;

namespace StreamCommunity.Application.Tests.ViewerGames.Enlistments.Handler;

[TestFixture]
public class ChangeEnlistmentSortOrderCommandHandlerTest : MockDbTestBase
{
    private ChangeEnlistmentSortOrderCommandHandler instance;
    private Mock<ILogger<ChangeEnlistmentSortOrderCommandHandler>> loggerMock;

    [SetUp]
    public void Setup()
    {
        loggerMock = new Mock<ILogger<ChangeEnlistmentSortOrderCommandHandler>>();
        instance = new ChangeEnlistmentSortOrderCommandHandler(DbContext, loggerMock.Object);
    }

    [TearDown]
    public void TearDown()
    {
        loggerMock.VerifyAll();
    }
    
    [Test]
    public async Task Handle_NoEnlistmentAvailable_JustLogsMessage()
    {
        var request = new ChangeEnlistmentSortOrderCommand(1, SortDirection.Up);

        var result = await instance.Handle(request, CancellationToken.None);
        Assert.AreEqual(Unit.Value,result);
        
        loggerMock.Verify(x => x.Log(
            It.Is<LogLevel>(l => l == LogLevel.Debug),
            It.IsAny<EventId>(),
            It.IsAny<It.IsAnyType>(),
            It.IsAny<Exception?>(),
            It.IsAny<Func<It.IsAnyType,Exception?,string>>()));
    }
    
    [Test]
    public async Task Handle_EnlistmentRequestedNotAvailable_JustLogsErrorMessage()
    {
        var enlistment = new Enlistment("testUser", DateTime.Now, 1);
        DbContext.Enlistments.Add(enlistment);
        await DbContext.SaveChangesAsync();
        
        var request = new ChangeEnlistmentSortOrderCommand(2, SortDirection.Up);

        var result = await instance.Handle(request, CancellationToken.None);
        Assert.AreEqual(Unit.Value,result);
        
        loggerMock.Verify(x => x.Log(
            It.Is<LogLevel>(l => l == LogLevel.Error),
            It.IsAny<EventId>(),
            It.IsAny<It.IsAnyType>(),
            It.IsAny<Exception?>(),
            It.IsAny<Func<It.IsAnyType,Exception?,string>>()));
    }
    
    [Test]
    public async Task Handle_WithMultipleEnlistments_EnlistmentToMoveUpIsFirst_DoesNotChangeSortOrder()
    {
        var enlistment = new Enlistment("testUser", DateTime.Now, 1);
        var enlistment2 = new Enlistment("testUser2", DateTime.Now, 2);
        DbContext.Enlistments.Add(enlistment);
        DbContext.Enlistments.Add(enlistment2);
        await DbContext.SaveChangesAsync();
        
        var request = new ChangeEnlistmentSortOrderCommand(enlistment.Id, SortDirection.Up);

        var result = await instance.Handle(request, CancellationToken.None);
        Assert.AreEqual(Unit.Value,result);
        
        Assert.AreEqual(1, enlistment.SortingNo);
    }
    
    [Test]
    public async Task Handle_WithMultipleEnlistments_EnlistmentToMoveDownIsLast_DoesNotChangeSortOrder()
    {
        var enlistment = new Enlistment("testUser", DateTime.Now, 1);
        var enlistment2 = new Enlistment("testUser2", DateTime.Now, 2);
        DbContext.Enlistments.Add(enlistment);
        DbContext.Enlistments.Add(enlistment2);
        await DbContext.SaveChangesAsync();
        
        var request = new ChangeEnlistmentSortOrderCommand(enlistment2.Id, SortDirection.Down);

        var result = await instance.Handle(request, CancellationToken.None);
        Assert.AreEqual(Unit.Value,result);
        
        Assert.AreEqual(2, enlistment2.SortingNo);
    }
    
    [Test]
    public async Task Handle_WithMultipleEnlistments_EnlistmentMoveUp_ChangesSortOrder()
    {
        var enlistment = new Enlistment("testUser", DateTime.Now, 1);
        var enlistment2 = new Enlistment("testUser2", DateTime.Now, 2);
        DbContext.Enlistments.Add(enlistment);
        DbContext.Enlistments.Add(enlistment2);
        await DbContext.SaveChangesAsync();
        
        var request = new ChangeEnlistmentSortOrderCommand(enlistment2.Id, SortDirection.Up);

        var result = await instance.Handle(request, CancellationToken.None);
        Assert.AreEqual(Unit.Value,result);
        
        Assert.AreEqual(1, enlistment2.SortingNo);
    }
    
    [Test]
    public async Task Handle_WithMultipleEnlistments_EnlistmentMoveDown_ChangesSortOrder()
    {
        var enlistment = new Enlistment("testUser", DateTime.Now, 1);
        var enlistment2 = new Enlistment("testUser2", DateTime.Now, 2);
        DbContext.Enlistments.Add(enlistment);
        DbContext.Enlistments.Add(enlistment2);
        await DbContext.SaveChangesAsync();
        
        var request = new ChangeEnlistmentSortOrderCommand(enlistment.Id, SortDirection.Down);

        var result = await instance.Handle(request, CancellationToken.None);
        Assert.AreEqual(Unit.Value,result);
        
        Assert.AreEqual(2, enlistment.SortingNo);
    }
    
    [Test]
    public async Task Handle_WithMultipleEnlistments_EnlistmentMoveDownWithWrongSortDirection_ThrowsArgumentOutOfRangeException()
    {
        var enlistment = new Enlistment("testUser", DateTime.Now, 1);
        var enlistment2 = new Enlistment("testUser2", DateTime.Now, 2);
        DbContext.Enlistments.Add(enlistment);
        DbContext.Enlistments.Add(enlistment2);
        await DbContext.SaveChangesAsync();
        
        var request = new ChangeEnlistmentSortOrderCommand(enlistment.Id, (SortDirection) 3);

        var exception = Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => instance.Handle(request, CancellationToken.None));
        Assert.AreEqual(nameof(request.SortDirection),exception?.ParamName);
        Assert.IsTrue(exception?.Message.StartsWith("Unknown value for direction"));        
    }
}
