using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekUserProfile.Persistense.EF.Migrations
{
    public partial class useQuestoNFiled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PriceCurrencyType",
                table: "UserQuestion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SystemPriceShare",
                table: "UserQuestion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TechnicianPriceShare",
                table: "UserQuestion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnitPrice",
                table: "UserQuestion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserQuestion_UserId",
                table: "UserQuestion",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserQuestion_User_UserId",
                table: "UserQuestion",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserQuestion_User_UserId",
                table: "UserQuestion");

            migrationBuilder.DropIndex(
                name: "IX_UserQuestion_UserId",
                table: "UserQuestion");

            migrationBuilder.DropColumn(
                name: "PriceCurrencyType",
                table: "UserQuestion");

            migrationBuilder.DropColumn(
                name: "SystemPriceShare",
                table: "UserQuestion");

            migrationBuilder.DropColumn(
                name: "TechnicianPriceShare",
                table: "UserQuestion");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "UserQuestion");
        }
    }
}
