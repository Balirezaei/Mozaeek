using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekCore.Persistense.EF.Migrations
{
    public partial class intPriceAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PriceAmount",
                table: "SubjectPriceHeader",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "PriceAmount",
                table: "RequestPriceHeader",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "PriceAmount",
                table: "SubjectPriceHeader",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "PriceAmount",
                table: "RequestPriceHeader",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
