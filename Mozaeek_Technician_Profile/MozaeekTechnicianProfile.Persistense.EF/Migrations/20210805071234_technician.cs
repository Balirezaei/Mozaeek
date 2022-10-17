using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekTechnicianProfile.Persistense.EF.Migrations
{
    public partial class technician : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TechnicianOnlineRequest");

            migrationBuilder.AddColumn<string>(
                name: "NationalNumber",
                table: "Technician",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PointId",
                table: "Technician",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TechnicianAttachement",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    CoreFileId = table.Column<long>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    TechnicianId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicianAttachement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicianAttachement_Technician_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "Technician",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TechnicianDefiniteRequestOrg",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestOrgId = table.Column<long>(nullable: false),
                    RequestOrgTitle = table.Column<string>(nullable: true),
                    TechnicianId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicianDefiniteRequestOrg", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicianDefiniteRequestOrg_Technician_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "Technician",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianAttachement_TechnicianId",
                table: "TechnicianAttachement",
                column: "TechnicianId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianDefiniteRequestOrg_TechnicianId",
                table: "TechnicianDefiniteRequestOrg",
                column: "TechnicianId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TechnicianAttachement");

            migrationBuilder.DropTable(
                name: "TechnicianDefiniteRequestOrg");

            migrationBuilder.DropColumn(
                name: "NationalNumber",
                table: "Technician");

            migrationBuilder.DropColumn(
                name: "PointId",
                table: "Technician");

            migrationBuilder.CreateTable(
                name: "TechnicianOnlineRequest",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<long>(type: "bigint", nullable: false),
                    RequestTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TechnicianId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicianOnlineRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicianOnlineRequest_Technician_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "Technician",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianOnlineRequest_TechnicianId",
                table: "TechnicianOnlineRequest",
                column: "TechnicianId");
        }
    }
}
