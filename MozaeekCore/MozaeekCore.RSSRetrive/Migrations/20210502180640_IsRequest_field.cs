using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekCore.RSSRetrive.Migrations
{
    public partial class IsRequest_field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AddColumn<bool>(
                name: "IsRequest",
                table: "RssNewses",
                nullable: false,
                defaultValue: false);



            migrationBuilder.AddColumn<bool>(
                name: "RequestIsProcessed",
                table: "RssNewses",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.DropColumn(
                name: "IsRequest",
                table: "RssNewses");

         
            migrationBuilder.DropColumn(
                name: "RequestIsProcessed",
                table: "RssNewses");

        }
    }
}
