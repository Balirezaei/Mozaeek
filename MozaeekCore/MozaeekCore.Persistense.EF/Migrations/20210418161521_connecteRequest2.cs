using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekCore.Persistense.EF.Migrations
{
    public partial class connecteRequest2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestConnection_Request_RequestId",
                table: "RequestConnection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestConnection",
                table: "RequestConnection");

            migrationBuilder.DropIndex(
                name: "IX_RequestConnection_RequestId",
                table: "RequestConnection");

            migrationBuilder.AlterColumn<long>(
                name: "RequestId",
                table: "RequestConnection",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestConnection",
                table: "RequestConnection",
                columns: new[] { "RequestId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_RequestConnection_Request_RequestId",
                table: "RequestConnection",
                column: "RequestId",
                principalTable: "Request",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestConnection_Request_RequestId",
                table: "RequestConnection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestConnection",
                table: "RequestConnection");

            migrationBuilder.AlterColumn<long>(
                name: "RequestId",
                table: "RequestConnection",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestConnection",
                table: "RequestConnection",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RequestConnection_RequestId",
                table: "RequestConnection",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestConnection_Request_RequestId",
                table: "RequestConnection",
                column: "RequestId",
                principalTable: "Request",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
