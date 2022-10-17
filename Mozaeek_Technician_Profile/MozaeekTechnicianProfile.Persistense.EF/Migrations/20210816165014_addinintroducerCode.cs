using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekTechnicianProfile.Persistense.EF.Migrations
{
    public partial class addinintroducerCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IntroducerCode",
                table: "Technician",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IntroducerCode",
                table: "Technician");
        }
    }
}
