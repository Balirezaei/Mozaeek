using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekUserProfile.Persistense.EF.Migrations
{
    public partial class updateUserQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TechnicianType",
                table: "UserQuestion",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TechnicianType",
                table: "UserQuestion");
        }
    }
}
