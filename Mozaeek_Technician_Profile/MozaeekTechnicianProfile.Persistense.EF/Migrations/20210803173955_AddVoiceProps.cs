using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekTechnicianProfile.Persistense.EF.Migrations
{
    public partial class AddVoiceProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionVicePath",
                table: "UserQuestionWaitingForTechnicians");

            migrationBuilder.AddColumn<long>(
                name: "VoiceFileId",
                table: "UserQuestionWaitingForTechnicians",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VoiceHttpPath",
                table: "UserQuestionWaitingForTechnicians",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VoiceFileId",
                table: "UserQuestionWaitingForTechnicians");

            migrationBuilder.DropColumn(
                name: "VoiceHttpPath",
                table: "UserQuestionWaitingForTechnicians");

            migrationBuilder.AddColumn<string>(
                name: "QuestionVicePath",
                table: "UserQuestionWaitingForTechnicians",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
