using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using StreamCommunity.Application.Common;
using StreamCommunity.Application.ViewerGames.EventHandler;
using StreamCommunity.Application.ViewerGames.Events;

namespace StreamCommunity.Application.Tests.ViewerGames.Enlistments.EventHandler;

public class PlayerEnlistedEventHandlerTest : MockDbTestBase
{
    private Mock<IChatMessaging> chatMessagingMock = null!;
    private PlayerEnlistedEventHandler instance = null!;

    [SetUp]
    public void SetUp()
    {
        chatMessagingMock = new Mock<IChatMessaging>();

        instance = new PlayerEnlistedEventHandler(DbContext, chatMessagingMock.Object);
    }

    [Test]
    public async Task Handle_WithNotification_ChatMessageIsSentAsync()
    {
        var userName = "testUser";

        await instance.Handle(new PlayerEnlisted(userName), CancellationToken.None);

        chatMessagingMock.Verify(
            x => x.SendMessageAsync(It.Is<string>(s => s.Contains(userName))));
    }
}