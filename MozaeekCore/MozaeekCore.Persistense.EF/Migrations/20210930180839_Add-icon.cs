using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekCore.Persistense.EF.Migrations
{
    public partial class Addicon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Subject",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "RequestTarget",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "RequestOrg",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "RequestTarget");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "RequestOrg");
        }
    }
}
