using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekUserProfile.Persistense.EF.Migrations
{
    public partial class Characteristic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "VoiceFileId",
                table: "UserQuestion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VoiceHttpPath",
                table: "UserQuestion",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DashboardType",
                table: "UserDashboard",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserProfileCharacteristicOwner",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileCharacteristicOwner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfileCharacteristicOwner_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserQuestionAttachment",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserQuestionId = table.Column<long>(nullable: false),
                    FileHttpAddress = table.Column<string>(maxLength: 100, nullable: true),
                    FileId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserQuestionAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserQuestionAttachment_UserQuestion_UserQuestionId",
                        column: x => x.UserQuestionId,
                        principalTable: "UserQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfileCharacteristic",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserProfileCharacteristicOwnerId = table.Column<int>(nullable: false),
                    FirstLabelParent = table.Column<string>(nullable: true),
                    FirstLabelParentId = table.Column<long>(nullable: false),
                    LabelId = table.Column<long>(nullable: false),
                    LabelTitle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileCharacteristic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfileCharacteristic_UserProfileCharacteristicOwner_UserProfileCharacteristicOwnerId",
                        column: x => x.UserProfileCharacteristicOwnerId,
                        principalTable: "UserProfileCharacteristicOwner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserDashboardCharacteristic",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserProfileCharacteristicId = table.Column<int>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    Title = table.Column<string>(maxLength: 40, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDashboardCharacteristic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDashboardCharacteristic_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDashboardCharacteristic_UserProfileCharacteristic_UserProfileCharacteristicId",
                        column: x => x.UserProfileCharacteristicId,
                        principalTable: "UserProfileCharacteristic",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDashboardCharacteristic_UserId",
                table: "UserDashboardCharacteristic",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDashboardCharacteristic_UserProfileCharacteristicId",
                table: "UserDashboardCharacteristic",
                column: "UserProfileCharacteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileCharacteristic_UserProfileCharacteristicOwnerId",
                table: "UserProfileCharacteristic",
                column: "UserProfileCharacteristicOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileCharacteristicOwner_UserId",
                table: "UserProfileCharacteristicOwner",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserQuestionAttachment_UserQuestionId",
                table: "UserQuestionAttachment",
                column: "UserQuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDashboardCharacteristic");

            migrationBuilder.DropTable(
                name: "UserQuestionAttachment");

            migrationBuilder.DropTable(
                name: "UserProfileCharacteristic");

            migrationBuilder.DropTable(
                name: "UserProfileCharacteristicOwner");

            migrationBuilder.DropColumn(
                name: "VoiceFileId",
                table: "UserQuestion");

            migrationBuilder.DropColumn(
                name: "VoiceHttpPath",
                table: "UserQuestion");

            migrationBuilder.DropColumn(
                name: "DashboardType",
                table: "UserDashboard");
        }
    }
}
