using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekUserProfile.Persistense.EF.Migrations
{
    public partial class userQuestionCompletion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "QuestionCode",
                startValue: 10000L);

            migrationBuilder.AddColumn<string>(
                name: "QuestionCode",
                table: "UserQuestion",
                nullable: true,
                defaultValueSql: "'MOZ'+Substring(convert(varchar, getdate(), 112),1,6) + convert(varchar,(NEXT VALUE FOR QuestionCode) )");

            migrationBuilder.AddColumn<string>(
                name: "RequestTitle",
                table: "UserQuestion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubjectTitle",
                table: "UserQuestion",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserWallets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    UserId1 = table.Column<long>(nullable: true),
                    PriceCurrencyTitle = table.Column<string>(maxLength: 50, nullable: true),
                    PriceCurrencyId = table.Column<int>(nullable: false),
                    AvailableAmount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWallets_User_UserId1",
                        column: x => x.UserId1,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserWalletCredit",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserWalletId = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    UserWalletCreditType = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWalletCredit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWalletCredit_UserWallets_UserWalletId",
                        column: x => x.UserWalletId,
                        principalTable: "UserWallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserWalletDebit",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    UserQuestionId = table.Column<long>(nullable: false),
                    UserWalletId = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWalletDebit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWalletDebit_UserQuestion_UserQuestionId",
                        column: x => x.UserQuestionId,
                        principalTable: "UserQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWalletDebit_UserWallets_UserWalletId",
                        column: x => x.UserWalletId,
                        principalTable: "UserWallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserWalletCredit_UserWalletId",
                table: "UserWalletCredit",
                column: "UserWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWalletDebit_UserQuestionId",
                table: "UserWalletDebit",
                column: "UserQuestionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserWalletDebit_UserWalletId",
                table: "UserWalletDebit",
                column: "UserWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWallets_UserId1",
                table: "UserWallets",
                column: "UserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWalletCredit");

            migrationBuilder.DropTable(
                name: "UserWalletDebit");

            migrationBuilder.DropTable(
                name: "UserWallets");

            migrationBuilder.DropSequence(
                name: "QuestionCode");

            migrationBuilder.DropColumn(
                name: "QuestionCode",
                table: "UserQuestion");

            migrationBuilder.DropColumn(
                name: "RequestTitle",
                table: "UserQuestion");

            migrationBuilder.DropColumn(
                name: "SubjectTitle",
                table: "UserQuestion");
        }
    }
}
