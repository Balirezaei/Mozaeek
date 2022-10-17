using Microsoft.EntityFrameworkCore.Migrations;

namespace Mozaeek.Notification.Persistence.Migrations
{
    public partial class PushNotificationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PushNotifications",
                table: "PushNotifications");

            migrationBuilder.RenameTable(
                name: "PushNotifications",
                newName: "PushNotification");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PushNotification",
                table: "PushNotification",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PushNotification",
                table: "PushNotification");

            migrationBuilder.RenameTable(
                name: "PushNotification",
                newName: "PushNotifications");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PushNotifications",
                table: "PushNotifications",
                column: "Id");
        }
    }
}
