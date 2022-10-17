using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekTechnicianProfile.Persistense.EF.Migrations
{
    public partial class UserQuestionWaitingForTechnician : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OtpCodes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MobileNo = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    ExpiredDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtpCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Technician",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    LastRefreshToken = table.Column<string>(nullable: true),
                    TechnicianType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technician", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserQuestionWaitingForTechnicians",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionCode = table.Column<string>(nullable: true),
                    UserFullName = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    QuestionId = table.Column<long>(nullable: false),
                    QuestionTextDescription = table.Column<string>(nullable: true),
                    QuestionVicePath = table.Column<string>(nullable: true),
                    QuestionType = table.Column<int>(nullable: false),
                    AnswerType = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<int>(nullable: false),
                    TechnicianType = table.Column<int>(nullable: false),
                    TechnicianPriceShare = table.Column<int>(nullable: false),
                    SystemPriceShare = table.Column<int>(nullable: false),
                    PriceCurrencyType = table.Column<int>(nullable: false),
                    UserQuestionState = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserQuestionWaitingForTechnicians", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TechnicianAbsencePresenceStateHistory",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    TechnicianAbsencePresenceState = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    TechnicianId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicianAbsencePresenceStateHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicianAbsencePresenceStateHistory_Technician_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "Technician",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TechnicianDiscount",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    TechnicianId = table.Column<long>(nullable: false),
                    DiscountAmount = table.Column<int>(nullable: false),
                    DiscountPercent = table.Column<short>(nullable: false),
                    FromDate = table.Column<DateTime>(nullable: false),
                    ToDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicianDiscount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicianDiscount_Technician_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "Technician",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TechnicianOflineRequest",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<long>(nullable: false),
                    RequestTitle = table.Column<string>(nullable: true),
                    TechnicianId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicianOflineRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicianOflineRequest_Technician_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "Technician",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TechnicianOnlineRequest",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<long>(nullable: false),
                    RequestTitle = table.Column<string>(nullable: true),
                    TechnicianId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicianOnlineRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicianOnlineRequest_Technician_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "Technician",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TechnicianSubject",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<long>(nullable: false),
                    SubjectTitle = table.Column<string>(nullable: true),
                    TechnicianId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicianSubject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicianSubject_Technician_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "Technician",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianAbsencePresenceStateHistory_TechnicianId",
                table: "TechnicianAbsencePresenceStateHistory",
                column: "TechnicianId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianDiscount_TechnicianId",
                table: "TechnicianDiscount",
                column: "TechnicianId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianOflineRequest_TechnicianId",
                table: "TechnicianOflineRequest",
                column: "TechnicianId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianOnlineRequest_TechnicianId",
                table: "TechnicianOnlineRequest",
                column: "TechnicianId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianSubject_TechnicianId",
                table: "TechnicianSubject",
                column: "TechnicianId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OtpCodes");

            migrationBuilder.DropTable(
                name: "TechnicianAbsencePresenceStateHistory");

            migrationBuilder.DropTable(
                name: "TechnicianDiscount");

            migrationBuilder.DropTable(
                name: "TechnicianOflineRequest");

            migrationBuilder.DropTable(
                name: "TechnicianOnlineRequest");

            migrationBuilder.DropTable(
                name: "TechnicianSubject");

            migrationBuilder.DropTable(
                name: "UserQuestionWaitingForTechnicians");

            migrationBuilder.DropTable(
                name: "Technician");
        }
    }
}
