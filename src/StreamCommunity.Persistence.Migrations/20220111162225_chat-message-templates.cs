using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamCommunity.Persistence.Migrations
{
    public partial class chatmessagetemplates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sc");

            migrationBuilder.CreateTable(
                name: "ChatMessageTemplates",
                schema: "sc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Identifier = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessageTemplates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessageTemplates_Identifier",
                schema: "sc",
                table: "ChatMessageTemplates",
                column: "Identifier",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessageTemplates",
                schema: "sc");
        }
    }
}
