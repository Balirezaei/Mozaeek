using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekCore.Persistense.EF.Migrations
{
    public partial class connecteRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_Request_RequestId",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Request_RequestId",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "Request");

            migrationBuilder.CreateTable(
                name: "RequestConnection",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConnectedRequestId = table.Column<long>(nullable: false),
                    RequestId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestConnection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestConnection_Request_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestConnection_RequestId",
                table: "RequestConnection",
                column: "RequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestConnection");

            migrationBuilder.AddColumn<long>(
                name: "RequestId",
                table: "Request",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Request_RequestId",
                table: "Request",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Request_RequestId",
                table: "Request",
                column: "RequestId",
                principalTable: "Request",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
