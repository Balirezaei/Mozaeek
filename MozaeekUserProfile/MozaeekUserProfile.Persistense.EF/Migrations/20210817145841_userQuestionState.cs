using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekUserProfile.Persistense.EF.Migrations
{
    public partial class userQuestionState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionState_UserQuestion_UserQuestionId",
                table: "QuestionState");

            migrationBuilder.DropForeignKey(
                name: "FK_UserQuestion_QuestionState_LastQuestionStateId",
                table: "UserQuestion");

            migrationBuilder.DropIndex(
                name: "IX_UserQuestion_LastQuestionStateId",
                table: "UserQuestion");

            migrationBuilder.DropColumn(
                name: "LastQuestionStateId",
                table: "UserQuestion");

            migrationBuilder.AddColumn<int>(
                name: "LastQuestionState",
                table: "UserQuestion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<long>(
                name: "UserQuestionId",
                table: "QuestionState",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionState_UserQuestion_UserQuestionId",
                table: "QuestionState",
                column: "UserQuestionId",
                principalTable: "UserQuestion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionState_UserQuestion_UserQuestionId",
                table: "QuestionState");

            migrationBuilder.DropColumn(
                name: "LastQuestionState",
                table: "UserQuestion");

            migrationBuilder.AddColumn<int>(
                name: "LastQuestionStateId",
                table: "UserQuestion",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UserQuestionId",
                table: "QuestionState",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_UserQuestion_LastQuestionStateId",
                table: "UserQuestion",
                column: "LastQuestionStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionState_UserQuestion_UserQuestionId",
                table: "QuestionState",
                column: "UserQuestionId",
                principalTable: "UserQuestion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserQuestion_QuestionState_LastQuestionStateId",
                table: "UserQuestion",
                column: "LastQuestionStateId",
                principalTable: "QuestionState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
