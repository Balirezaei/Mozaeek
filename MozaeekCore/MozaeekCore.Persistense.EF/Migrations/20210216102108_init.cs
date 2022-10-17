using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekCore.Persistense.EF.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExecutiveTechnicians",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    NationalCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExecutiveTechnicians", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Label",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 50, nullable: true),
                    ParentId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Label", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Label_Label_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Label",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Point",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 50, nullable: true),
                    ParentId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Point", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Point_Point_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Point",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestAct",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestAct", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestOrg",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 50, nullable: true),
                    ParentId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestOrg", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestOrg_RequestOrg_ParentId",
                        column: x => x.ParentId,
                        principalTable: "RequestOrg",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestTarget",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestTarget", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RSS",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(maxLength: 150, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Source = table.Column<string>(maxLength: 50, nullable: true),
                    IntervalDataReceiveHours = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RSS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 80, nullable: true),
                    ParentId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subject_Subject_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnProcessedRequests",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Summery = table.Column<string>(nullable: true),
                    IsProcessed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnProcessedRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestTargetId = table.Column<long>(nullable: false),
                    RequestActId = table.Column<long>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    RequestExpiredDate = table.Column<DateTime>(nullable: true),
                    RequestStartDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Request_RequestAct_RequestActId",
                        column: x => x.RequestActId,
                        principalTable: "RequestAct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Request_RequestTarget_RequestTargetId",
                        column: x => x.RequestTargetId,
                        principalTable: "RequestTarget",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestTargetLabel",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestTargetId = table.Column<long>(nullable: false),
                    LabelId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestTargetLabel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestTargetLabel_Label_LabelId",
                        column: x => x.LabelId,
                        principalTable: "Label",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RequestTargetLabel_RequestTarget_RequestTargetId",
                        column: x => x.RequestTargetId,
                        principalTable: "RequestTarget",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestTargetRequestOrg",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestTargetId = table.Column<long>(nullable: false),
                    RequestOrgId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestTargetRequestOrg", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestTargetRequestOrg_RequestOrg_RequestOrgId",
                        column: x => x.RequestOrgId,
                        principalTable: "RequestOrg",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RequestTargetRequestOrg_RequestTarget_RequestTargetId",
                        column: x => x.RequestTargetId,
                        principalTable: "RequestTarget",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestTargetSubject",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestTargetId = table.Column<long>(nullable: false),
                    SubjectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestTargetSubject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestTargetSubject_RequestTarget_RequestTargetId",
                        column: x => x.RequestTargetId,
                        principalTable: "RequestTarget",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestTargetSubject_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequestAction",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Priority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestAction", x => new { x.RequestId, x.Id });
                    table.ForeignKey(
                        name: "FK_RequestAction_Request_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestDocument",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Priority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestDocument", x => new { x.RequestId, x.Id });
                    table.ForeignKey(
                        name: "FK_RequestDocument_Request_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestNessesity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Priority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestNessesity", x => new { x.RequestId, x.Id });
                    table.ForeignKey(
                        name: "FK_RequestNessesity_Request_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestQualification",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Priority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestQualification", x => new { x.RequestId, x.Id });
                    table.ForeignKey(
                        name: "FK_RequestQualification_Request_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Label_ParentId",
                table: "Label",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Point_ParentId",
                table: "Point",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_RequestActId",
                table: "Request",
                column: "RequestActId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_RequestTargetId",
                table: "Request",
                column: "RequestTargetId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestOrg_ParentId",
                table: "RequestOrg",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestTargetLabel_LabelId",
                table: "RequestTargetLabel",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestTargetLabel_RequestTargetId",
                table: "RequestTargetLabel",
                column: "RequestTargetId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestTargetRequestOrg_RequestOrgId",
                table: "RequestTargetRequestOrg",
                column: "RequestOrgId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestTargetRequestOrg_RequestTargetId",
                table: "RequestTargetRequestOrg",
                column: "RequestTargetId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestTargetSubject_RequestTargetId",
                table: "RequestTargetSubject",
                column: "RequestTargetId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestTargetSubject_SubjectId",
                table: "RequestTargetSubject",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_ParentId",
                table: "Subject",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExecutiveTechnicians");

            migrationBuilder.DropTable(
                name: "Point");

            migrationBuilder.DropTable(
                name: "RequestAction");

            migrationBuilder.DropTable(
                name: "RequestDocument");

            migrationBuilder.DropTable(
                name: "RequestNessesity");

            migrationBuilder.DropTable(
                name: "RequestQualification");

            migrationBuilder.DropTable(
                name: "RequestTargetLabel");

            migrationBuilder.DropTable(
                name: "RequestTargetRequestOrg");

            migrationBuilder.DropTable(
                name: "RequestTargetSubject");

            migrationBuilder.DropTable(
                name: "RSS");

            migrationBuilder.DropTable(
                name: "UnProcessedRequests");

            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropTable(
                name: "Label");

            migrationBuilder.DropTable(
                name: "RequestOrg");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "RequestAct");

            migrationBuilder.DropTable(
                name: "RequestTarget");
        }
    }
}
