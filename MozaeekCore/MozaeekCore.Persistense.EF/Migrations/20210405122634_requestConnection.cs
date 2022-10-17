using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekCore.Persistense.EF.Migrations
{
    public partial class requestConnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RequestId",
                table: "Request",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
