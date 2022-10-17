using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekCore.Persistense.EF.Migrations
{
    public partial class technician : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NationalNumber",
                table: "Technician");

            migrationBuilder.AddColumn<string>(
                name: "NationalId",
                table: "Technician",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Technician",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NationalId",
                table: "Technician");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Technician");

            migrationBuilder.AddColumn<string>(
                name: "NationalNumber",
                table: "Technician",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
