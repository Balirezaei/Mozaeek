using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekCore.Persistense.EF.Migrations
{
    public partial class renameExteion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileExtention",
                table: "TechnicianAttachment");

            migrationBuilder.DropColumn(
                name: "FileExtention",
                table: "Announcement");

            migrationBuilder.AddColumn<string>(
                name: "FileExtension",
                table: "TechnicianAttachment",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileExtension",
                table: "Announcement",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileExtension",
                table: "TechnicianAttachment");

            migrationBuilder.DropColumn(
                name: "FileExtension",
                table: "Announcement");

            migrationBuilder.AddColumn<string>(
                name: "FileExtention",
                table: "TechnicianAttachment",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileExtention",
                table: "Announcement",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }
    }
}
