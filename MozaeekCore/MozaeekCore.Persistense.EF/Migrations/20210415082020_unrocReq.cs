using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekCore.Persistense.EF.Migrations
{
    public partial class unrocReq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UnProcessedRequests",
                table: "UnProcessedRequests");

            migrationBuilder.RenameTable(
                name: "UnProcessedRequests",
                newName: "UnProcessedRequest");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "UnProcessedRequest",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnProcessedRequest",
                table: "UnProcessedRequest",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UnProcessedRequest",
                table: "UnProcessedRequest");

            migrationBuilder.RenameTable(
                name: "UnProcessedRequest",
                newName: "UnProcessedRequests");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "UnProcessedRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnProcessedRequests",
                table: "UnProcessedRequests",
                column: "Id");
        }
    }
}
