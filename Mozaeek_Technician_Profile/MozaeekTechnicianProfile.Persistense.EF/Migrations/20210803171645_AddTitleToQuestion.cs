using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekTechnicianProfile.Persistense.EF.Migrations
{
    public partial class AddTitleToQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QuestionTitle",
                table: "UserQuestionWaitingForTechnicians",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionTitle",
                table: "UserQuestionWaitingForTechnicians");
        }
    }
}
