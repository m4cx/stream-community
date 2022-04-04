using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using StreamCommunity.Application.Common;
using StreamCommunity.Application.ViewerGames.EventHandler;
using StreamCommunity.Application.ViewerGames.Events;

namespace StreamCommunity.Application.Tests.ViewerGames.Enlistments.EventHandler;

public class PlayerWithdrawFailedEventHandlerTest : MockDbTestBase
{
    private Mock<IChatMessaging> chatMessagingMock = null!;
    private PlayerWithdrawFailedEventHandler instance = null!;

    [SetUp]
    public void SetUp()
    {
        chatMessagingMock = new Mock<IChatMessaging>();

        instance = new PlayerWithdrawFailedEventHandler(DbContext, chatMessagingMock.Object);
    }

    [Test]
    public async Task Handle_WithNotification_ChatMessageIsSentAsync()
    {
        var userName = "testUser";

        await instance.Handle(
            new PlayerWithdrawelFailed(
                userName,
                PlayerWithdrawelFailedReason.PlayerNotFound),
            CancellationToken.None);

        chatMessagingMock.Verify(
            x => x.SendMessageAsync(It.Is<string>(s => s.Contains(userName))));
    }
}
