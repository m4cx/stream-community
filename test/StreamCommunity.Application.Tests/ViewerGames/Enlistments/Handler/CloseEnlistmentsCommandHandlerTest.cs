using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using StreamCommunity.Application.Persistence;
using StreamCommunity.Application.ViewerGames.Enlistments;
using StreamCommunity.Application.ViewerGames.Enlistments.Handler;

namespace StreamCommunity.Application.Tests.ViewerGames.Enlistments.Handler;

[TestFixture]
public class CloseEnlistmentsCommandHandlerTest : MockDbTestBase
{
    private CloseEnlistmentsCommandHandler instance;
    private Mock<IStreamCommunityContext> contextMock;

    [SetUp]
    public void Setup()
    {
        contextMock = new Mock<IStreamCommunityContext>();
        instance = new CloseEnlistmentsCommandHandler(contextMock.Object);
    }

    [Test]
    public async Task Handle()
    {
        await instance.Handle(new CloseEnlistmentsCommand(new[] { 123 }), CancellationToken.None);
    } 
}