using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamCommunity.Persistence.Migrations
{
    public partial class AddEnlistmentSorting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SortingNo",
                schema: "tc",
                table: "Enlistments",
                type: "INTEGER",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortingNo",
                schema: "tc",
                table: "Enlistments");
        }
    }
}
