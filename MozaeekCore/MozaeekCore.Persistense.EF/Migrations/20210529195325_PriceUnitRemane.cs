using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekCore.Persistense.EF.Migrations
{
    public partial class PriceUnitRemane : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestPriceHeader_PriceUnit_PriceUnitId",
                table: "RequestPriceHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectPriceHeader_PriceUnit_PriceUnitId1",
                table: "SubjectPriceHeader");

            migrationBuilder.DropTable(
                name: "PriceUnit");

            migrationBuilder.DropIndex(
                name: "IX_SubjectPriceHeader_PriceUnitId1",
                table: "SubjectPriceHeader");

            migrationBuilder.DropIndex(
                name: "IX_RequestPriceHeader_PriceUnitId",
                table: "RequestPriceHeader");

            migrationBuilder.DropColumn(
                name: "PriceUnitId",
                table: "SubjectPriceHeader");

            migrationBuilder.DropColumn(
                name: "PriceUnitId1",
                table: "SubjectPriceHeader");

            migrationBuilder.DropColumn(
                name: "IsRequest",
                table: "RssNewses");

            migrationBuilder.DropColumn(
                name: "RequestIsProcessed",
                table: "RssNewses");

            migrationBuilder.DropColumn(
                name: "PriceUnitId",
                table: "RequestPriceHeader");

            migrationBuilder.AddColumn<long>(
                name: "PriceCurrencyId",
                table: "SubjectPriceHeader",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PriceCurrencyId",
                table: "RequestPriceHeader",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "PriceCurrency",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Unit = table.Column<string>(maxLength: 15, nullable: true),
                    CurrencyCode = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceCurrency", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PriceCurrency",
                columns: new[] { "Id", "CurrencyCode", "Unit" },
                values: new object[] { 1L, "IRR", "ریال" });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectPriceHeader_PriceCurrencyId",
                table: "SubjectPriceHeader",
                column: "PriceCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestPriceHeader_PriceCurrencyId",
                table: "RequestPriceHeader",
                column: "PriceCurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestPriceHeader_PriceCurrency_PriceCurrencyId",
                table: "RequestPriceHeader",
                column: "PriceCurrencyId",
                principalTable: "PriceCurrency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectPriceHeader_PriceCurrency_PriceCurrencyId",
                table: "SubjectPriceHeader",
                column: "PriceCurrencyId",
                principalTable: "PriceCurrency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestPriceHeader_PriceCurrency_PriceCurrencyId",
                table: "RequestPriceHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectPriceHeader_PriceCurrency_PriceCurrencyId",
                table: "SubjectPriceHeader");

            migrationBuilder.DropTable(
                name: "PriceCurrency");

            migrationBuilder.DropIndex(
                name: "IX_SubjectPriceHeader_PriceCurrencyId",
                table: "SubjectPriceHeader");

            migrationBuilder.DropIndex(
                name: "IX_RequestPriceHeader_PriceCurrencyId",
                table: "RequestPriceHeader");

            migrationBuilder.DropColumn(
                name: "PriceCurrencyId",
                table: "SubjectPriceHeader");

            migrationBuilder.DropColumn(
                name: "PriceCurrencyId",
                table: "RequestPriceHeader");

            migrationBuilder.AddColumn<int>(
                name: "PriceUnitId",
                table: "SubjectPriceHeader",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "PriceUnitId1",
                table: "SubjectPriceHeader",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRequest",
                table: "RssNewses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequestIsProcessed",
                table: "RssNewses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "PriceUnitId",
                table: "RequestPriceHeader",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "PriceUnit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceUnit", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PriceUnit",
                columns: new[] { "Id", "CurrencyCode", "Unit" },
                values: new object[] { 1L, "IRR", "ریال" });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectPriceHeader_PriceUnitId1",
                table: "SubjectPriceHeader",
                column: "PriceUnitId1");

            migrationBuilder.CreateIndex(
                name: "IX_RequestPriceHeader_PriceUnitId",
                table: "RequestPriceHeader",
                column: "PriceUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestPriceHeader_PriceUnit_PriceUnitId",
                table: "RequestPriceHeader",
                column: "PriceUnitId",
                principalTable: "PriceUnit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectPriceHeader_PriceUnit_PriceUnitId1",
                table: "SubjectPriceHeader",
                column: "PriceUnitId1",
                principalTable: "PriceUnit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
