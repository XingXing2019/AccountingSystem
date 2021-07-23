using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountingDatabase.Migrations
{
    public partial class ChangeVendorName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VendName",
                table: "Vendors",
                newName: "VendorName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VendorName",
                table: "Vendors",
                newName: "VendName");
        }
    }
}
