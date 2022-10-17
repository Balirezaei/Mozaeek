using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekCore.Persistense.EF.Migrations
{
    public partial class renameSummaryInRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn("Summary", "Request", "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
