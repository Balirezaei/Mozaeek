using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekCore.Persistense.EF.Migrations
{
    public partial class changeAnnouncement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcement_RequestTarget_RequestTargetId",
                table: "Announcement");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_TechnicianDefiniteRequestOrg_DefiniteRequestOrg_DefiniteRequestOrgId",
            //    table: "TechnicianDefiniteRequestOrg");

            //migrationBuilder.DropIndex(
            //    name: "IX_TechnicianDefiniteRequestOrg_DefiniteRequestOrgId",
            //    table: "TechnicianDefiniteRequestOrg");

            migrationBuilder.DropIndex(
                name: "IX_Announcement_RequestTargetId",
                table: "Announcement");

            //migrationBuilder.DropColumn(
            //    name: "DefiniteRequestOrgId",
            //    table: "TechnicianDefiniteRequestOrg");

            migrationBuilder.DropColumn(
                name: "RequestTargetId",
                table: "Announcement");

            //migrationBuilder.AddColumn<long>(
            //    name: "DefiniteeRequestOrgId",
            //    table: "TechnicianDefiniteRequestOrg",
            //    nullable: false,
            //    defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "AnnouncementLabel",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnnouncementId = table.Column<long>(nullable: false),
                    LabelId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementLabel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnouncementLabel_Announcement_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalTable: "Announcement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnnouncementLabel_Label_LabelId",
                        column: x => x.LabelId,
                        principalTable: "Label",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementRequestOrg",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnnouncementId = table.Column<long>(nullable: false),
                    RequestOrgId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementRequestOrg", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnouncementRequestOrg_Announcement_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalTable: "Announcement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnnouncementRequestOrg_RequestOrg_RequestOrgId",
                        column: x => x.RequestOrgId,
                        principalTable: "RequestOrg",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementSubject",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnnouncementId = table.Column<long>(nullable: false),
                    SubjectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementSubject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnouncementSubject_Announcement_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalTable: "Announcement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnnouncementSubject_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id");
                });

            //migrationBuilder.CreateIndex(
            //    name: "IX_TechnicianDefiniteRequestOrg_DefiniteeRequestOrgId",
            //    table: "TechnicianDefiniteRequestOrg",
            //    column: "DefiniteeRequestOrgId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementLabel_AnnouncementId",
                table: "AnnouncementLabel",
                column: "AnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementLabel_LabelId",
                table: "AnnouncementLabel",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementRequestOrg_AnnouncementId",
                table: "AnnouncementRequestOrg",
                column: "AnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementRequestOrg_RequestOrgId",
                table: "AnnouncementRequestOrg",
                column: "RequestOrgId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementSubject_AnnouncementId",
                table: "AnnouncementSubject",
                column: "AnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementSubject_SubjectId",
                table: "AnnouncementSubject",
                column: "SubjectId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_TechnicianDefiniteRequestOrg_DefiniteRequestOrg_DefiniteeRequestOrgId",
            //    table: "TechnicianDefiniteRequestOrg",
            //    column: "DefiniteeRequestOrgId",
            //    principalTable: "DefiniteRequestOrg",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechnicianDefiniteRequestOrg_DefiniteRequestOrg_DefiniteeRequestOrgId",
                table: "TechnicianDefiniteRequestOrg");

            migrationBuilder.DropTable(
                name: "AnnouncementLabel");

            migrationBuilder.DropTable(
                name: "AnnouncementRequestOrg");

            migrationBuilder.DropTable(
                name: "AnnouncementSubject");

            migrationBuilder.DropIndex(
                name: "IX_TechnicianDefiniteRequestOrg_DefiniteeRequestOrgId",
                table: "TechnicianDefiniteRequestOrg");

            migrationBuilder.DropColumn(
                name: "DefiniteeRequestOrgId",
                table: "TechnicianDefiniteRequestOrg");

            migrationBuilder.AddColumn<long>(
                name: "DefiniteRequestOrgId",
                table: "TechnicianDefiniteRequestOrg",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "RequestTargetId",
                table: "Announcement",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianDefiniteRequestOrg_DefiniteRequestOrgId",
                table: "TechnicianDefiniteRequestOrg",
                column: "DefiniteRequestOrgId");

            migrationBuilder.CreateIndex(
                name: "IX_Announcement_RequestTargetId",
                table: "Announcement",
                column: "RequestTargetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcement_RequestTarget_RequestTargetId",
                table: "Announcement",
                column: "RequestTargetId",
                principalTable: "RequestTarget",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicianDefiniteRequestOrg_DefiniteRequestOrg_DefiniteRequestOrgId",
                table: "TechnicianDefiniteRequestOrg",
                column: "DefiniteRequestOrgId",
                principalTable: "DefiniteRequestOrg",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
