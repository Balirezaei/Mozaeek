using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekTechnicianProfile.Persistense.EF.Migrations
{
    public partial class AddPropsToQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RequestId",
                table: "UserQuestionWaitingForTechnicians",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestTitle",
                table: "UserQuestionWaitingForTechnicians",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SubjectId",
                table: "UserQuestionWaitingForTechnicians",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubjectTitle",
                table: "UserQuestionWaitingForTechnicians",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "UserQuestionWaitingForTechnicians");

            migrationBuilder.DropColumn(
                name: "RequestTitle",
                table: "UserQuestionWaitingForTechnicians");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "UserQuestionWaitingForTechnicians");

            migrationBuilder.DropColumn(
                name: "SubjectTitle",
                table: "UserQuestionWaitingForTechnicians");
        }
    }
}
