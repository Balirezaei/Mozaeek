using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekCore.Persistense.EF.Migrations
{
    public partial class add_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDocument",
                table: "RequestTarget",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Regulation",
                table: "Request",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "Request",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "Announcement",
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDocument",
                table: "RequestTarget");

            migrationBuilder.DropColumn(
                name: "Regulation",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "Announcement");
        }
    }
}
