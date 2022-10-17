using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekUserProfile.Persistense.EF.Migrations
{
    public partial class title_point : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "UserPoint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "UserPoint");
        }
    }
}
