using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekCore.Persistense.EF.Migrations
{
    public partial class lenTarget_announcePoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementPoint_Announcement_AnnouncementId",
                table: "AnnouncementPoint");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "RequestTarget",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementPoint_Announcement_AnnouncementId",
                table: "AnnouncementPoint",
                column: "AnnouncementId",
                principalTable: "Announcement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementPoint_Announcement_AnnouncementId",
                table: "AnnouncementPoint");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "RequestTarget",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementPoint_Announcement_AnnouncementId",
                table: "AnnouncementPoint",
                column: "AnnouncementId",
                principalTable: "Announcement",
                principalColumn: "Id");
        }
    }
}
