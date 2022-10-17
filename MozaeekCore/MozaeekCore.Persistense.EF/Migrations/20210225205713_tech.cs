using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekCore.Persistense.EF.Migrations
{
    public partial class tech : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExecutiveTechnicians");

            migrationBuilder.CreateTable(
                name: "EducationField",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    ParentId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationField", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationField_EducationField_ParentId",
                        column: x => x.ParentId,
                        principalTable: "EducationField",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EducationGrade",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationGrade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TechnicianContactInfo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MobileNumber = table.Column<string>(maxLength: 20, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: true),
                    OfficeNumber = table.Column<string>(maxLength: 20, nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Address = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicianContactInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TechnicianPersonalInfo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: true),
                    NationalCode = table.Column<string>(maxLength: 20, nullable: true),
                    IdentityNumber = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicianPersonalInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TechnicianEducationalInfo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EducationGradeId = table.Column<long>(nullable: false),
                    EducationFieldId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicianEducationalInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicianEducationalInfo_EducationField_EducationFieldId",
                        column: x => x.EducationFieldId,
                        principalTable: "EducationField",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TechnicianEducationalInfo_EducationGrade_EducationGradeId",
                        column: x => x.EducationGradeId,
                        principalTable: "EducationGrade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Technician",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    TechnicianType = table.Column<byte>(type: "TINYINT", nullable: false),
                    TechnicianContactInfoId = table.Column<long>(nullable: true),
                    TechnicianEducationalInformationId = table.Column<long>(nullable: true),
                    TechnicianPersonalInfoId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technician", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Technician_TechnicianContactInfo_TechnicianContactInfoId",
                        column: x => x.TechnicianContactInfoId,
                        principalTable: "TechnicianContactInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Technician_TechnicianEducationalInfo_TechnicianEducationalInformationId",
                        column: x => x.TechnicianEducationalInformationId,
                        principalTable: "TechnicianEducationalInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Technician_TechnicianPersonalInfo_TechnicianPersonalInfoId",
                        column: x => x.TechnicianPersonalInfoId,
                        principalTable: "TechnicianPersonalInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TechnicianAttachment",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttachmentType = table.Column<int>(nullable: false),
                    Source = table.Column<byte[]>(nullable: true),
                    FileName = table.Column<string>(maxLength: 100, nullable: true),
                    FileExtention = table.Column<string>(maxLength: 50, nullable: true),
                    TechnicianId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicianAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicianAttachment_Technician_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "Technician",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TechnicianPoint",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PointId = table.Column<long>(nullable: false),
                    TechnicianId = table.Column<long>(nullable: false)
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
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<long>(nullable: false),
                    TechnicianId = table.Column<long>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "TechnicianSubject",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<long>(nullable: false),
                    TechnicianId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicianSubject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicianSubject_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TechnicianSubject_Technician_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "Technician",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EducationField_ParentId",
                table: "EducationField",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Technician_TechnicianContactInfoId",
                table: "Technician",
                column: "TechnicianContactInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Technician_TechnicianEducationalInformationId",
                table: "Technician",
                column: "TechnicianEducationalInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_Technician_TechnicianPersonalInfoId",
                table: "Technician",
                column: "TechnicianPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianAttachment_TechnicianId",
                table: "TechnicianAttachment",
                column: "TechnicianId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianEducationalInfo_EducationFieldId",
                table: "TechnicianEducationalInfo",
                column: "EducationFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianEducationalInfo_EducationGradeId",
                table: "TechnicianEducationalInfo",
                column: "EducationGradeId");

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

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianSubject_SubjectId",
                table: "TechnicianSubject",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianSubject_TechnicianId",
                table: "TechnicianSubject",
                column: "TechnicianId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TechnicianAttachment");

            migrationBuilder.DropTable(
                name: "TechnicianPoint");

            migrationBuilder.DropTable(
                name: "TechnicianRequest");

            migrationBuilder.DropTable(
                name: "TechnicianSubject");

            migrationBuilder.DropTable(
                name: "Technician");

            migrationBuilder.DropTable(
                name: "TechnicianContactInfo");

            migrationBuilder.DropTable(
                name: "TechnicianEducationalInfo");

            migrationBuilder.DropTable(
                name: "TechnicianPersonalInfo");

            migrationBuilder.DropTable(
                name: "EducationField");

            migrationBuilder.DropTable(
                name: "EducationGrade");

            migrationBuilder.CreateTable(
                name: "ExecutiveTechnicians",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExecutiveTechnicians", x => x.Id);
                });
        }
    }
}
