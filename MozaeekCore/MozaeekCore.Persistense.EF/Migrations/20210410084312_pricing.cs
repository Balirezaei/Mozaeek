using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekCore.Persistense.EF.Migrations
{
    public partial class pricing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriceUnit",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Unit = table.Column<string>(maxLength: 15, nullable: true),
                    CurrencyCode = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceUnit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestPriceHeader",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PriceUnitId = table.Column<long>(nullable: false),
                    PriceAmount = table.Column<long>(nullable: false),
                    Title = table.Column<string>(maxLength: 150, nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    SystemShare = table.Column<short>(nullable: false),
                    TechnicianShare = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestPriceHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestPriceHeader_PriceUnit_PriceUnitId",
                        column: x => x.PriceUnitId,
                        principalTable: "PriceUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectPriceHeader",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PriceUnitId1 = table.Column<long>(nullable: true),
                    PriceUnitId = table.Column<int>(nullable: false),
                    PriceAmount = table.Column<long>(nullable: false),
                    Title = table.Column<string>(maxLength: 150, nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    SystemShare = table.Column<short>(nullable: false),
                    TechnicianShare = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectPriceHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectPriceHeader_PriceUnit_PriceUnitId1",
                        column: x => x.PriceUnitId1,
                        principalTable: "PriceUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestPriceDetails",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestPriceHeaderId = table.Column<long>(nullable: false),
                    RequestId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestPriceDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestPriceDetails_Request_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestPriceDetails_RequestPriceHeader_RequestPriceHeaderId",
                        column: x => x.RequestPriceHeaderId,
                        principalTable: "RequestPriceHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectPriceDetails",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectPriceHeaderId = table.Column<long>(nullable: false),
                    SubjectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectPriceDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectPriceDetails_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectPriceDetails_SubjectPriceHeader_SubjectPriceHeaderId",
                        column: x => x.SubjectPriceHeaderId,
                        principalTable: "SubjectPriceHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PriceUnit",
                columns: new[] { "Id", "CurrencyCode", "Unit" },
                values: new object[] { 1L, "IRR", "ریال" });

            migrationBuilder.CreateIndex(
                name: "IX_RequestPriceDetails_RequestId",
                table: "RequestPriceDetails",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestPriceDetails_RequestPriceHeaderId",
                table: "RequestPriceDetails",
                column: "RequestPriceHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestPriceHeader_PriceUnitId",
                table: "RequestPriceHeader",
                column: "PriceUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectPriceDetails_SubjectId",
                table: "SubjectPriceDetails",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectPriceDetails_SubjectPriceHeaderId",
                table: "SubjectPriceDetails",
                column: "SubjectPriceHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectPriceHeader_PriceUnitId1",
                table: "SubjectPriceHeader",
                column: "PriceUnitId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestPriceDetails");

            migrationBuilder.DropTable(
                name: "SubjectPriceDetails");

            migrationBuilder.DropTable(
                name: "RequestPriceHeader");

            migrationBuilder.DropTable(
                name: "SubjectPriceHeader");

            migrationBuilder.DropTable(
                name: "PriceUnit");
        }
    }
}
