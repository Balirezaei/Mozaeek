using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekCore.Persistense.EF.Migrations
{
    public partial class announcement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Announcement",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    RequestTargetId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Announcement_RequestTarget_RequestTargetId",
                        column: x => x.RequestTargetId,
                        principalTable: "RequestTarget",
                        principalColumn: "Id");
                });

            //migrationBuilder.CreateTable(
            //    name: "RssNewses",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ModifiedDate = table.Column<DateTime>(nullable: false),
            //        Title = table.Column<string>(nullable: true),
            //        IsProcessed = table.Column<bool>(nullable: false),
            //        Link = table.Column<string>(nullable: true),
            //        Description = table.Column<string>(nullable: true),
            //        CreateDate = table.Column<DateTime>(nullable: false),
            //        Source = table.Column<string>(nullable: true),
            //        RSSId = table.Column<long>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RssNewses", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "AnnouncementPoint",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PointId = table.Column<long>(nullable: false),
                    AnnouncementId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementPoint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnouncementPoint_Announcement_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalTable: "Announcement",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AnnouncementPoint_Point_PointId",
                        column: x => x.PointId,
                        principalTable: "Point",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Announcement_RequestTargetId",
                table: "Announcement",
                column: "RequestTargetId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementPoint_AnnouncementId",
                table: "AnnouncementPoint",
                column: "AnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementPoint_PointId",
                table: "AnnouncementPoint",
                column: "PointId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnouncementPoint");

            //migrationBuilder.DropTable(
            //    name: "RssNewses");

            migrationBuilder.DropTable(
                name: "Announcement");
        }
    }
}
