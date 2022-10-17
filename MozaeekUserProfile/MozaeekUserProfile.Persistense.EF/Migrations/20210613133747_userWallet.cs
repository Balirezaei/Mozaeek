using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekUserProfile.Persistense.EF.Migrations
{
    public partial class userWallet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWallets_User_UserId1",
                table: "UserWallets");

            migrationBuilder.DropIndex(
                name: "IX_UserWallets_UserId1",
                table: "UserWallets");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserWallets");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "UserWallets",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "QuestionUniqId",
                table: "UserQuestion",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UserWallets_UserId",
                table: "UserWallets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserWallets_User_UserId",
                table: "UserWallets",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWallets_User_UserId",
                table: "UserWallets");

            migrationBuilder.DropIndex(
                name: "IX_UserWallets_UserId",
                table: "UserWallets");

            migrationBuilder.DropColumn(
                name: "QuestionUniqId",
                table: "UserQuestion");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserWallets",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "UserId1",
                table: "UserWallets",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserWallets_UserId1",
                table: "UserWallets",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserWallets_User_UserId1",
                table: "UserWallets",
                column: "UserId1",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
