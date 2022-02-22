using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Migrations;
using StreamCommunity.Application;
using StreamCommunity.Application.ChatMessages;

#nullable disable

namespace StreamCommunity.Persistence.Migrations
{
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    public partial class chatmessagetemplatesdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ChatMessageTemplates",
                schema: "sc",
                columns: new[]
                {
                    "Id", "Identifier", "Message"
                },
                values: new object[,]
                {
                    {
                        1, ChatMessageTemplateIdentifiers.PlayerEnlistedEventHandler,
                        "Viewer Game erfolgreich für {UserName} vorgemerkt."
                    },
                    {
                        2, ChatMessageTemplateIdentifiers.PlayerEnlistmentFailedEventHandler,
                        "Viewer Game konnte nicht für {UserName} vorgemerkt werden. {Reason}"
                    }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ChatMessageTemplates",
                schema: "sc",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2 });
        }
    }
}