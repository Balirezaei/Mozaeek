using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekUserProfile.Persistense.EF.Migrations
{
    public partial class updateUserdashboard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDashboards_User_UserId",
                table: "UserDashboards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDashboards",
                table: "UserDashboards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OtpCodes",
                table: "OtpCodes");

            migrationBuilder.RenameTable(
                name: "UserDashboards",
                newName: "UserDashboard");

            migrationBuilder.RenameTable(
                name: "OtpCodes",
                newName: "OtpCode");

            migrationBuilder.RenameIndex(
                name: "IX_UserDashboards_UserId",
                table: "UserDashboard",
                newName: "IX_UserDashboard_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "UserDashboard",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MobileNo",
                table: "OtpCode",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "OtpCode",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDashboard",
                table: "UserDashboard",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OtpCode",
                table: "OtpCode",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDashboard_User_UserId",
                table: "UserDashboard",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDashboard_User_UserId",
                table: "UserDashboard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDashboard",
                table: "UserDashboard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OtpCode",
                table: "OtpCode");

            migrationBuilder.RenameTable(
                name: "UserDashboard",
                newName: "UserDashboards");

            migrationBuilder.RenameTable(
                name: "OtpCode",
                newName: "OtpCodes");

            migrationBuilder.RenameIndex(
                name: "IX_UserDashboard_UserId",
                table: "UserDashboards",
                newName: "IX_UserDashboards_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "UserDashboards",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MobileNo",
                table: "OtpCodes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 12);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "OtpCodes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 8);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDashboards",
                table: "UserDashboards",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OtpCodes",
                table: "OtpCodes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDashboards_User_UserId",
                table: "UserDashboards",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
