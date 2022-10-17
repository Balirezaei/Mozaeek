using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekCore.Persistense.EF.Migrations
{
    public partial class technicinatOfflineRequestTargetId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechnicianOfflineRequestTarget_RequestTarget_RquestTargetIdId",
                table: "TechnicianOfflineRequestTarget");

            migrationBuilder.DropIndex(
                name: "IX_TechnicianOfflineRequestTarget_RquestTargetIdId",
                table: "TechnicianOfflineRequestTarget");

            migrationBuilder.DropColumn(
                name: "RquestTargetIdId",
                table: "TechnicianOfflineRequestTarget");

            migrationBuilder.AddColumn<long>(
                name: "RquestTargetId",
                table: "TechnicianOfflineRequestTarget",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianOfflineRequestTarget_RquestTargetId",
                table: "TechnicianOfflineRequestTarget",
                column: "RquestTargetId");

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicianOfflineRequestTarget_RequestTarget_RquestTargetId",
                table: "TechnicianOfflineRequestTarget",
                column: "RquestTargetId",
                principalTable: "RequestTarget",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechnicianOfflineRequestTarget_RequestTarget_RquestTargetId",
                table: "TechnicianOfflineRequestTarget");

            migrationBuilder.DropIndex(
                name: "IX_TechnicianOfflineRequestTarget_RquestTargetId",
                table: "TechnicianOfflineRequestTarget");

            migrationBuilder.DropColumn(
                name: "RquestTargetId",
                table: "TechnicianOfflineRequestTarget");

            migrationBuilder.AddColumn<long>(
                name: "RquestTargetIdId",
                table: "TechnicianOfflineRequestTarget",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianOfflineRequestTarget_RquestTargetIdId",
                table: "TechnicianOfflineRequestTarget",
                column: "RquestTargetIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicianOfflineRequestTarget_RequestTarget_RquestTargetIdId",
                table: "TechnicianOfflineRequestTarget",
                column: "RquestTargetIdId",
                principalTable: "RequestTarget",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
