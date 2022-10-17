using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekCore.Persistense.EF.Migrations
{
    public partial class FileTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FileId",
                table: "Announcement",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 150, nullable: true),
                    Path = table.Column<string>(maxLength: 400, nullable: true),
                    Extension = table.Column<string>(maxLength: 20, nullable: true),
                    Type = table.Column<int>(maxLength: 20, nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Announcement_FileId",
                table: "Announcement",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcement_File_FileId",
                table: "Announcement",
                column: "FileId",
                principalTable: "File",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcement_File_FileId",
                table: "Announcement");

            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropIndex(
                name: "IX_Announcement_FileId",
                table: "Announcement");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Announcement");
        }
    }
}
