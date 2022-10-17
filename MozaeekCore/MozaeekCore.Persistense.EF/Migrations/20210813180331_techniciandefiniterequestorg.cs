using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekCore.Persistense.EF.Migrations
{
    public partial class TechnicianDefiniterequestorg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_TechnicianDefiniteRequestOrg_RequestOrg_RequestOrgId",
            //    table: "TechnicianDefiniteRequestOrg");

            //migrationBuilder.DropIndex(
            //    name: "IX_TechnicianDefiniteRequestOrg_RequestOrgId",
            //    table: "TechnicianDefiniteRequestOrg");

            //migrationBuilder.DropColumn(
            //    name: "RequestOrgId",
            //    table: "TechnicianDefiniteRequestOrg");

            //migrationBuilder.AddColumn<long>(
            //    name: "DefiniteRequestOrgId",
            //    table: "TechnicianDefiniteRequestOrg",
            //    nullable: false,
            //    defaultValue: 0L);

            //migrationBuilder.CreateIndex(
            //    name: "IX_TechnicianDefiniteRequestOrg_DefiniteRequestOrgId",
            //    table: "TechnicianDefiniteRequestOrg",
            //    column: "DefiniteRequestOrgId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_TechnicianDefiniteRequestOrg_DefiniteRequestOrg_DefiniteRequestOrgId",
            //    table: "TechnicianDefiniteRequestOrg",
            //    column: "DefiniteRequestOrgId",
            //    principalTable: "DefiniteRequestOrg",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechnicianDefiniteRequestOrg_DefiniteRequestOrg_DefiniteRequestOrgId",
                table: "TechnicianDefiniteRequestOrg");

            migrationBuilder.DropIndex(
                name: "IX_TechnicianDefiniteRequestOrg_DefiniteRequestOrgId",
                table: "TechnicianDefiniteRequestOrg");

            migrationBuilder.DropColumn(
                name: "DefiniteRequestOrgId",
                table: "TechnicianDefiniteRequestOrg");

            migrationBuilder.AddColumn<long>(
                name: "RequestOrgId",
                table: "TechnicianDefiniteRequestOrg",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianDefiniteRequestOrg_RequestOrgId",
                table: "TechnicianDefiniteRequestOrg",
                column: "RequestOrgId");

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicianDefiniteRequestOrg_RequestOrg_RequestOrgId",
                table: "TechnicianDefiniteRequestOrg",
                column: "RequestOrgId",
                principalTable: "RequestOrg",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
