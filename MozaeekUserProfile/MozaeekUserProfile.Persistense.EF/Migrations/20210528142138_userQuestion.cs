using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekUserProfile.Persistense.EF.Migrations
{
    public partial class userQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserQuestion",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<long>(nullable: true),
                    SubjectId = table.Column<long>(nullable: true),
                    TextDescription = table.Column<string>(maxLength: 1500, nullable: true),
                    QuestionType = table.Column<int>(nullable: false),
                    AnswerType = table.Column<int>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    LastQuestionStateId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserQuestion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionState",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 1500, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    SerializedEvent = table.Column<string>(nullable: true),
                    UserQuestionState = table.Column<int>(nullable: false),
                    UserQuestionId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionState", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionState_UserQuestion_UserQuestionId",
                        column: x => x.UserQuestionId,
                        principalTable: "UserQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionState_UserQuestionId",
                table: "QuestionState",
                column: "UserQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserQuestion_LastQuestionStateId",
                table: "UserQuestion",
                column: "LastQuestionStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserQuestion_QuestionState_LastQuestionStateId",
                table: "UserQuestion",
                column: "LastQuestionStateId",
                principalTable: "QuestionState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionState_UserQuestion_UserQuestionId",
                table: "QuestionState");

            migrationBuilder.DropTable(
                name: "UserQuestion");

            migrationBuilder.DropTable(
                name: "QuestionState");
        }
    }
}
