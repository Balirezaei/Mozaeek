using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekUserProfile.Persistense.EF.Migrations
{
    public partial class addingdeviceid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QuestionTitle",
                table: "UserQuestion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeviceId",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionTitle",
                table: "UserQuestion");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "User");
        }
    }
}
