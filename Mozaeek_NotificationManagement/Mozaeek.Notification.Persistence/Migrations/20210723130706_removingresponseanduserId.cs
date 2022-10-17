using Microsoft.EntityFrameworkCore.Migrations;

namespace Mozaeek.Notification.Persistence.Migrations
{
    public partial class removingresponseanduserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Response",
                table: "PushNotification");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PushNotification");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Response",
                table: "PushNotification",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "PushNotification",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
