using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using StreamCommunity.Application.ViewerGames;
using StreamCommunity.Application.ViewerGames.Handler;
using StreamCommunity.Domain;
using StreamCommunity.Domain.Exceptions;

namespace StreamCommunity.Application.Tests.ViewerGames.Enlistments.Handler;

[TestFixture]
public class CloseEnlistmentsCommandHandlerTest : MockDbTestBase
{
    private CloseEnlistmentsCommandHandler instance = null!;

    [SetUp]
    public void SetUp()
    {
        instance = new CloseEnlistmentsCommandHandler(DbContext);
    }

    [Test]
    public async Task Handle_EnlistmentInStateActive_IsClosedAsync()
    {
        var enlistment = new Enlistment("testUser", DateTime.Now);
        enlistment.Draw();
        DbContext.Enlistments.Add(enlistment);
        await DbContext.SaveChangesAsync();
        
        await instance.Handle(new CloseEnlistmentsCommand(new[] { enlistment.Id }), CancellationToken.None);
        
        Assert.AreEqual(EnlistmentState.Closed, enlistment.State);
    } 
    
    [Test]
    public async Task Handle_EnlistmentNotInStateActive_ThrowsEnlistmentExceptionAsync()
    {
        var enlistment = new Enlistment("testUser", DateTime.Now);
        DbContext.Enlistments.Add(enlistment);
        await DbContext.SaveChangesAsync();
        
        Assert.ThrowsAsync<EnlistmentException>(() => instance.Handle(new CloseEnlistmentsCommand(new[] { enlistment.Id }), CancellationToken.None));
        
        enlistment.Draw();
        enlistment.Close();
        await DbContext.SaveChangesAsync();
        
        Assert.ThrowsAsync<EnlistmentException>(() => instance.Handle(new CloseEnlistmentsCommand(new[] { enlistment.Id }), CancellationToken.None));
    } 
}