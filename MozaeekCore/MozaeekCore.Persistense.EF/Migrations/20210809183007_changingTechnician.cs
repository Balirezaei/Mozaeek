using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekCore.Persistense.EF.Migrations
{
    public partial class changingTechnician : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Technician_TechnicianEducationalInfo_TechnicianEducationalInformationId",
                table: "Technician");

            migrationBuilder.DropForeignKey(
                name: "FK_Technician_TechnicianPersonalInfo_TechnicianPersonalInfoId",
                table: "Technician");

            migrationBuilder.DropTable(
                name: "TechnicianPoint");

            migrationBuilder.DropTable(
                name: "TechnicianRequest");

            migrationBuilder.DropIndex(
                name: "IX_Technician_TechnicianEducationalInformationId",
                table: "Technician");

            migrationBuilder.DropColumn(
                name: "AttachmentType",
                table: "TechnicianAttachment");

            migrationBuilder.DropColumn(
                name: "FileExtension",
                table: "TechnicianAttachment");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "TechnicianAttachment");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "TechnicianAttachment");

            migrationBuilder.DropColumn(
                name: "CreateDateTime",
                table: "Technician");

            migrationBuilder.DropColumn(
                name: "TechnicianEducationalInformationId",
                table: "Technician");

            migrationBuilder.AddColumn<long>(
                name: "FileId",
                table: "TechnicianAttachment",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<int>(
                name: "TechnicianType",
                table: "Technician",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "TINYINT");

            migrationBuilder.AlterColumn<long>(
                name: "TechnicianPersonalInfoId",
                table: "Technician",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Technician",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Technician",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Technician",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Technician",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NationalNumber",
                table: "Technician",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Technician",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PointId",
                table: "Technician",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TechnicianEducationalInfoId",
                table: "Technician",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DefiniteRequestOrg",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestOrgId = table.Column<long>(nullable: false),
                    PointId = table.Column<long>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefiniteRequestOrg", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DefiniteRequestOrg_Point_PointId",
                        column: x => x.PointId,
                        principalTable: "Point",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DefiniteRequestOrg_RequestOrg_RequestOrgId",
                        column: x => x.RequestOrgId,
                        principalTable: "RequestOrg",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TechnicianDefiniteRequestOrg",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestOrgId = table.Column<long>(nullable: false),
                    TechnicianId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicianDefiniteRequestOrg", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicianDefiniteRequestOrg_RequestOrg_RequestOrgId",
                        column: x => x.RequestOrgId,
                        principalTable: "RequestOrg",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TechnicianDefiniteRequestOrg_Technician_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "Technician",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TechnicianOfflineRequestTarget",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RquestTargetIdId = table.Column<long>(nullable: true),
                    TechnicianId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicianOfflineRequestTarget", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicianOfflineRequestTarget_RequestTarget_RquestTargetIdId",
                        column: x => x.RquestTargetIdId,
                        principalTable: "RequestTarget",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TechnicianOfflineRequestTarget_Technician_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "Technician",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianAttachment_FileId",
                table: "TechnicianAttachment",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Technician_TechnicianEducationalInfoId",
                table: "Technician",
                column: "TechnicianEducationalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_DefiniteRequestOrg_PointId",
                table: "DefiniteRequestOrg",
                column: "PointId");

            migrationBuilder.CreateIndex(
                name: "IX_DefiniteRequestOrg_RequestOrgId",
                table: "DefiniteRequestOrg",
                column: "RequestOrgId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianDefiniteRequestOrg_RequestOrgId",
                table: "TechnicianDefiniteRequestOrg",
                column: "RequestOrgId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianDefiniteRequestOrg_TechnicianId",
                table: "TechnicianDefiniteRequestOrg",
                column: "TechnicianId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianOfflineRequestTarget_RquestTargetIdId",
                table: "TechnicianOfflineRequestTarget",
                column: "RquestTargetIdId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianOfflineRequestTarget_TechnicianId",
                table: "TechnicianOfflineRequestTarget",
                column: "TechnicianId");

            migrationBuilder.AddForeignKey(
                name: "FK_Technician_TechnicianEducationalInfo_TechnicianEducationalInfoId",
                table: "Technician",
                column: "TechnicianEducationalInfoId",
                principalTable: "TechnicianEducationalInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Technician_TechnicianPersonalInfo_TechnicianPersonalInfoId",
                table: "Technician",
                column: "TechnicianPersonalInfoId",
                principalTable: "TechnicianPersonalInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicianAttachment_File_FileId",
                table: "TechnicianAttachment",
                column: "FileId",
                principalTable: "File",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Technician_TechnicianEducationalInfo_TechnicianEducationalInfoId",
                table: "Technician");

            migrationBuilder.DropForeignKey(
                name: "FK_Technician_TechnicianPersonalInfo_TechnicianPersonalInfoId",
                table: "Technician");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicianAttachment_File_FileId",
                table: "TechnicianAttachment");

            migrationBuilder.DropTable(
                name: "DefiniteRequestOrg");

            migrationBuilder.DropTable(
                name: "TechnicianDefiniteRequestOrg");

            migrationBuilder.DropTable(
                name: "TechnicianOfflineRequestTarget");

            migrationBuilder.DropIndex(
                name: "IX_TechnicianAttachment_FileId",
                table: "TechnicianAttachment");

            migrationBuilder.DropIndex(
                name: "IX_Technician_TechnicianEducationalInfoId",
                table: "Technician");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "TechnicianAttachment");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Technician");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Technician");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Technician");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Technician");

            migrationBuilder.DropColumn(
                name: "NationalNumber",
                table: "Technician");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Technician");

            migrationBuilder.DropColumn(
                name: "PointId",
                table: "Technician");

            migrationBuilder.DropColumn(
                name: "TechnicianEducationalInfoId",
                table: "Technician");

            migrationBuilder.AddColumn<int>(
                name: "AttachmentType",
                table: "TechnicianAttachment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FileExtension",
                table: "TechnicianAttachment",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "TechnicianAttachment",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Source",
                table: "TechnicianAttachment",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "TechnicianType",
                table: "Technician",
                type: "TINYINT",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "TechnicianPersonalInfoId",
                table: "Technician",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDateTime",
                table: "Technician",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "TechnicianEducationalInformationId",
                table: "Technician",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TechnicianPoint",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PointId = table.Column<long>(type: "bigint", nullable: false),
                    TechnicianId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicianPoint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicianPoint_Point_PointId",
                        column: x => x.PointId,
                        principalTable: "Point",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TechnicianPoint_Technician_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "Technician",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TechnicianRequest",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<long>(type: "bigint", nullable: false),
                    TechnicianId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicianRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicianRequest_Request_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TechnicianRequest_Technician_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "Technician",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Technician_TechnicianEducationalInformationId",
                table: "Technician",
                column: "TechnicianEducationalInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianPoint_PointId",
                table: "TechnicianPoint",
                column: "PointId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianPoint_TechnicianId",
                table: "TechnicianPoint",
                column: "TechnicianId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianRequest_RequestId",
                table: "TechnicianRequest",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianRequest_TechnicianId",
                table: "TechnicianRequest",
                column: "TechnicianId");

            migrationBuilder.AddForeignKey(
                name: "FK_Technician_TechnicianEducationalInfo_TechnicianEducationalInformationId",
                table: "Technician",
                column: "TechnicianEducationalInformationId",
                principalTable: "TechnicianEducationalInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Technician_TechnicianPersonalInfo_TechnicianPersonalInfoId",
                table: "Technician",
                column: "TechnicianPersonalInfoId",
                principalTable: "TechnicianPersonalInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
