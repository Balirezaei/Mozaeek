using Microsoft.EntityFrameworkCore.Migrations;

namespace MozaeekUserProfile.Persistense.EF.Migrations
{
    public partial class Characteristic_rename_LabelParent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstLabelParent",
                table: "UserProfileCharacteristic");

            migrationBuilder.AddColumn<string>(
                name: "FirstLabelParentTitle",
                table: "UserProfileCharacteristic",
                maxLength: 40,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstLabelParentTitle",
                table: "UserProfileCharacteristic");

            migrationBuilder.AddColumn<string>(
                name: "FirstLabelParent",
                table: "UserProfileCharacteristic",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);
        }
    }
}
