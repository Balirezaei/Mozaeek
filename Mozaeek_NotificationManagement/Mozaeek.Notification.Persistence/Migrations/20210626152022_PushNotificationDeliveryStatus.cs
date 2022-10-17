using Microsoft.EntityFrameworkCore.Migrations;

namespace Mozaeek.Notification.Persistence.Migrations
{
    public partial class PushNotificationDeliveryStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "deliveryStatus",
                table: "PushNotification",
                newName: "DeliveryStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeliveryStatus",
                table: "PushNotification",
                newName: "deliveryStatus");
        }
    }
}
