using System;
using NUnit.Framework;
using StreamCommunity.Domain.Exceptions;

namespace StreamCommunity.Domain.Tests;

[TestFixture]
public class EnlistmentTest
{
    private Enlistment enlistment = null!;

    [SetUp]
    public void Setup()
    {
        enlistment = new Enlistment("testUser", DateTime.Now);
    }
    
    [Test]
    public void Draw_StateIsActive_ThrowsEnlistmentException()
    {
        
        enlistment.Draw();

        Assert.Throws<EnlistmentException>(() => enlistment.Draw());
    }

    [Test]
    public void Draw_StateIsClosed_ThrowsEnlistmentException()
    {
        enlistment.Draw();
        enlistment.Close();
        
        Assert.Throws<EnlistmentException>(() => enlistment.Draw());
    }

    [Test]
    public void Draw_StateIsOpen_StateIsSetToActive()
    {
        enlistment.Draw();
        Assert.AreEqual(EnlistmentState.Active, enlistment.State);
    }

    [Test]
    public void Close_StateIsOpen_ThrowsEnlistmentException()
    {
        Assert.Throws<EnlistmentException>(() => enlistment.Close());
    }
    
    [Test]
    public void Close_StateIsClosed_ThrowsEnlistmentException()
    {
        enlistment.Draw();
        enlistment.Close();
        Assert.Throws<EnlistmentException>(() => enlistment.Close());
    }
    
    [Test]
    public void Draw_StateIsActive_StateIsSetToClosed()
    {
        enlistment.Draw();
        enlistment.Close();
        Assert.AreEqual(EnlistmentState.Closed, enlistment.State);
    }
}