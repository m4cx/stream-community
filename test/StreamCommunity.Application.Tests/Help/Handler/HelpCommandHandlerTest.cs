using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using StreamCommunity.Application.ChatMessages;
using StreamCommunity.Application.Common;
using StreamCommunity.Application.Help;
using StreamCommunity.Application.Help.Handler;
using StreamCommunity.Domain;

namespace StreamCommunity.Application.Tests.Help.Handler;

[TestFixture]
public class HelpCommandHandlerTest : MockDbTestBase
{
    private ChatMessageTemplate helpMessageTemplate = null!;
    private Mock<IChatMessaging> chatMessagingMock = null!;
    private HelpCommandHandler instance = null!;

    [SetUp]
    public async Task Initialize()
    {
        await InitHelpData();

        chatMessagingMock = new Mock<IChatMessaging>();
        instance = new HelpCommandHandler(DbContext, chatMessagingMock.Object);
    }

    [Test]
    public async Task Handle_WithHelpCommand_SendsHelpMessageThroughChatMessaging()
    {
        var command = new HelpCommand();
        await instance.Handle(command, CancellationToken.None);

        chatMessagingMock.Verify(
            m => m.SendMessageAsync(
                It.Is<string>(s => s == helpMessageTemplate.Message)),
            Times.Once);
    }

    private async Task InitHelpData()
    {
        helpMessageTemplate = new ChatMessageTemplate(ChatMessageTemplateIdentifiers.Help, "A help message");
        DbContext.ChatMessageTemplates.Add(helpMessageTemplate);
        await DbContext.SaveChangesAsync();
    }
}