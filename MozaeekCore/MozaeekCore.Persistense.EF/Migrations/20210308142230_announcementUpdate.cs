using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekCore.Persistense.EF.Migrations
{
    public partial class announcementUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileExtention",
                table: "Announcement",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Announcement",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Source",
                table: "Announcement",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileExtention",
                table: "Announcement");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Announcement");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "Announcement");
        }
    }
}
