using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekCore.Persistense.EF.Migrations
{
    public partial class removeRequestOrgFromTarget : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "RequestTargetRequestOrg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {



        }
    }
}
