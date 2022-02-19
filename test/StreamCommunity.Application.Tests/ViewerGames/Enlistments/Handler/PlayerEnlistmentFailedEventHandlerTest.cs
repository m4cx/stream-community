using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using StreamCommunity.Application.Common;
using StreamCommunity.Application.ViewerGames.Enlistments.Events;
using StreamCommunity.Application.ViewerGames.Enlistments.Handler;

namespace StreamCommunity.Application.Tests.ViewerGames.Enlistments.Handler;

public class PlayerEnlistmentFailedEventHandlerTest : MockDbTestBase
{
    private Mock<IChatMessaging> chatMessagingMock = null!;
    private PlayerEnlistmentFailedEventHandler instance = null!;

    [SetUp]
    public void SetUp()
    {
        chatMessagingMock = new Mock<IChatMessaging>();

        instance = new PlayerEnlistmentFailedEventHandler(DbContext, chatMessagingMock.Object);
    }

    [Test]
    public async Task Handle_WithNotification_ChatMessageIsSentAsync()
    {
        var userName = "testUser";

        await instance.Handle(new PlayerEnlistmentFailed(userName), CancellationToken.None);

        chatMessagingMock.Verify(
            x => x.SendMessageAsync(It.Is<string>(s => s.Contains(userName))));
    }
}