using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekCore.Persistense.EF.Migrations
{
    public partial class TechnicianPoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Technician_PointId",
                table: "Technician",
                column: "PointId");

            migrationBuilder.AddForeignKey(
                name: "FK_Technician_Point_PointId",
                table: "Technician",
                column: "PointId",
                principalTable: "Point",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Technician_Point_PointId",
                table: "Technician");

            migrationBuilder.DropIndex(
                name: "IX_Technician_PointId",
                table: "Technician");
        }
    }
}
