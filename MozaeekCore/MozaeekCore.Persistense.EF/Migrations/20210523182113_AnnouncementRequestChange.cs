using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekCore.Persistense.EF.Migrations
{
    public partial class AnnouncementRequestChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasRequest",
                table: "Announcement",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "RequestId",
                table: "Announcement",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Announcement_RequestId",
                table: "Announcement",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcement_Request_RequestId",
                table: "Announcement",
                column: "RequestId",
                principalTable: "Request",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcement_Request_RequestId",
                table: "Announcement");

            migrationBuilder.DropIndex(
                name: "IX_Announcement_RequestId",
                table: "Announcement");

            migrationBuilder.DropColumn(
                name: "HasRequest",
                table: "Announcement");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "Announcement");
        }
    }
}
