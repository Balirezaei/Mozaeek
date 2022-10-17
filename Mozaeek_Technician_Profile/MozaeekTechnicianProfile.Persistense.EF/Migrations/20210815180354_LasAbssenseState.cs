using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekTechnicianProfile.Persistense.EF.Migrations
{
    public partial class LasAbssenseState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LastAbsencePresenceState",
                table: "Technician",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastAbsencePresenceState",
                table: "Technician");
        }
    }
}
