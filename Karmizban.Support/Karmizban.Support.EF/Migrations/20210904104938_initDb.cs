using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Karmizban.Support.EF.Migrations
{
    public partial class initDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TechnicianSuggestedSupport",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 350, nullable: true),
                    Description = table.Column<string>(maxLength: 2500, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicianSuggestedSupport", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSuggestedSupport",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 350, nullable: true),
                    Description = table.Column<string>(maxLength: 2500, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSuggestedSupport", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TechnicianRequestSupport",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Technician_TechnicianId = table.Column<long>(nullable: true),
                    Technician_TechnicianFullName = table.Column<string>(nullable: true),
                    TechnicianSuggestedSupportId = table.Column<long>(nullable: true),
                    QuestionId = table.Column<long>(nullable: false),
                    QuestionCode = table.Column<string>(maxLength: 20, nullable: true),
                    Title = table.Column<string>(maxLength: 350, nullable: true),
                    Description = table.Column<string>(maxLength: 2500, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsAnswered = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicianRequestSupport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicianRequestSupport_TechnicianSuggestedSupport_TechnicianSuggestedSupportId",
                        column: x => x.TechnicianSuggestedSupportId,
                        principalTable: "TechnicianSuggestedSupport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRequestSupport",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_UserId = table.Column<long>(nullable: true),
                    User_UserFullName = table.Column<string>(nullable: true),
                    UserSuggestedSupportId = table.Column<long>(nullable: true),
                    QuestionId = table.Column<long>(nullable: false),
                    QuestionCode = table.Column<string>(maxLength: 20, nullable: true),
                    Title = table.Column<string>(maxLength: 350, nullable: true),
                    Description = table.Column<string>(maxLength: 2500, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsAnswered = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRequestSupport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRequestSupport_UserSuggestedSupport_UserSuggestedSupportId",
                        column: x => x.UserSuggestedSupportId,
                        principalTable: "UserSuggestedSupport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TechnicianRequestSupportAnswer",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TechnicianRequestSupportId = table.Column<long>(nullable: false),
                    AnswerDescription = table.Column<string>(maxLength: 2500, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicianRequestSupportAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicianRequestSupportAnswer_TechnicianRequestSupport_TechnicianRequestSupportId",
                        column: x => x.TechnicianRequestSupportId,
                        principalTable: "TechnicianRequestSupport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRequestSupportAnswer",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserRequestSupportId = table.Column<long>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    AnswerDescription = table.Column<string>(maxLength: 2500, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRequestSupportAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRequestSupportAnswer_UserRequestSupport_UserRequestSupportId",
                        column: x => x.UserRequestSupportId,
                        principalTable: "UserRequestSupport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianRequestSupport_TechnicianSuggestedSupportId",
                table: "TechnicianRequestSupport",
                column: "TechnicianSuggestedSupportId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianRequestSupportAnswer_TechnicianRequestSupportId",
                table: "TechnicianRequestSupportAnswer",
                column: "TechnicianRequestSupportId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRequestSupport_UserSuggestedSupportId",
                table: "UserRequestSupport",
                column: "UserSuggestedSupportId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRequestSupportAnswer_UserRequestSupportId",
                table: "UserRequestSupportAnswer",
                column: "UserRequestSupportId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TechnicianRequestSupportAnswer");

            migrationBuilder.DropTable(
                name: "UserRequestSupportAnswer");

            migrationBuilder.DropTable(
                name: "TechnicianRequestSupport");

            migrationBuilder.DropTable(
                name: "UserRequestSupport");

            migrationBuilder.DropTable(
                name: "TechnicianSuggestedSupport");

            migrationBuilder.DropTable(
                name: "UserSuggestedSupport");
        }
    }
}
