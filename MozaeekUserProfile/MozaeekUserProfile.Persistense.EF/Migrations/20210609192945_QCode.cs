using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekUserProfile.Persistense.EF.Migrations
{
    public partial class QCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionCode",
                table: "UserQuestion");

            migrationBuilder.AddColumn<string>(
                name: "QuestionCodeNo",
                table: "UserQuestion",
                maxLength: 6,
                nullable: true,
                defaultValueSql: "convert(varchar,(NEXT VALUE FOR QuestionCode) )");

            migrationBuilder.AddColumn<string>(
                name: "QuestionCodePreFix",
                table: "UserQuestion",
                maxLength: 3,
                nullable: true);

            migrationBuilder.RestartSequence(
                name: "QuestionCode",
                startValue: 100000L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionCodeNo",
                table: "UserQuestion");

            migrationBuilder.DropColumn(
                name: "QuestionCodePreFix",
                table: "UserQuestion");

            migrationBuilder.AddColumn<string>(
                name: "QuestionCode",
                table: "UserQuestion",
                type: "nvarchar(max)",
                nullable: true,
                defaultValueSql: "'MOZ'+Substring(convert(varchar, getdate(), 112),1,6) + convert(varchar,(NEXT VALUE FOR QuestionCode) )");

            migrationBuilder.RestartSequence(
                name: "QuestionCode",
                startValue: 10000L);
        }
    }
}
