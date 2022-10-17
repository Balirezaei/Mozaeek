using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekCore.Persistense.EF.Migrations
{
    public partial class definitRequestOrgToRequestDomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestDocument");

            migrationBuilder.CreateTable(
                name: "RequestDefiniteRequestOrg",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DefiniteRequestOrgId = table.Column<long>(nullable: false),
                    RequestId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestDefiniteRequestOrg", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestDefiniteRequestOrg_DefiniteRequestOrg_DefiniteRequestOrgId",
                        column: x => x.DefiniteRequestOrgId,
                        principalTable: "DefiniteRequestOrg",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestDefiniteRequestOrg_Request_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestRequestOrg",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestOrgId = table.Column<long>(nullable: false),
                    RequestId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestRequestOrg", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestRequestOrg_Request_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestRequestOrg_RequestOrg_RequestOrgId",
                        column: x => x.RequestOrgId,
                        principalTable: "RequestOrg",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestDefiniteRequestOrg_DefiniteRequestOrgId",
                table: "RequestDefiniteRequestOrg",
                column: "DefiniteRequestOrgId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestDefiniteRequestOrg_RequestId",
                table: "RequestDefiniteRequestOrg",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestRequestOrg_RequestId",
                table: "RequestRequestOrg",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestRequestOrg_RequestOrgId",
                table: "RequestRequestOrg",
                column: "RequestOrgId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestDefiniteRequestOrg");

            migrationBuilder.DropTable(
                name: "RequestRequestOrg");

            migrationBuilder.CreateTable(
                name: "RequestDocument",
                columns: table => new
                {
                    RequestId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestDocument", x => new { x.RequestId, x.Id });
                    table.ForeignKey(
                        name: "FK_RequestDocument_Request_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
