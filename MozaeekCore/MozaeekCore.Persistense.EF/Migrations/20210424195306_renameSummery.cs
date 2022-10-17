using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekCore.Persistense.EF.Migrations
{
    public partial class renameSummery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Summery",
                table: "PreRequest");

            migrationBuilder.AddColumn<string>(
                name: "LastExpiredToken",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "PreRequest",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastExpiredToken",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "PreRequest");

            migrationBuilder.AddColumn<string>(
                name: "Summery",
                table: "PreRequest",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
