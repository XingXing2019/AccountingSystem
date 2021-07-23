using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountingDatabase.Migrations
{
    public partial class ChangeColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TextSTRE4",
                table: "Vendors",
                newName: "Address4");

            migrationBuilder.RenameColumn(
                name: "TextSTRE3",
                table: "Vendors",
                newName: "Address3");

            migrationBuilder.RenameColumn(
                name: "TextSTRE2",
                table: "Vendors",
                newName: "Address2");

            migrationBuilder.RenameColumn(
                name: "TextSTRE1",
                table: "Vendors",
                newName: "Address1");

            migrationBuilder.RenameColumn(
                name: "TextPHON2",
                table: "Vendors",
                newName: "Phone2");

            migrationBuilder.RenameColumn(
                name: "TextPHON1",
                table: "Vendors",
                newName: "Phone1");

            migrationBuilder.RenameColumn(
                name: "SwHold",
                table: "Vendors",
                newName: "OnHold");

            migrationBuilder.RenameColumn(
                name: "SwActv",
                table: "Vendors",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "NameCity",
                table: "Vendors",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "DateStart",
                table: "Vendors",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "DateLastMN",
                table: "Vendors",
                newName: "LastMaintenanceDate");

            migrationBuilder.RenameColumn(
                name: "CurnCode",
                table: "Vendors",
                newName: "CurrencyCode");

            migrationBuilder.RenameColumn(
                name: "CodeSTTE",
                table: "Vendors",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "CodePSTL",
                table: "Vendors",
                newName: "PostCode");

            migrationBuilder.RenameColumn(
                name: "CodeCTRY",
                table: "Vendors",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "TransDate",
                table: "Transactions",
                newName: "TransactionDate");

            migrationBuilder.RenameColumn(
                name: "PostSeq",
                table: "Transactions",
                newName: "PostSequence");

            migrationBuilder.RenameColumn(
                name: "ExchRate",
                table: "Transactions",
                newName: "ExchangeRate");

            migrationBuilder.RenameColumn(
                name: "Config",
                table: "GlAccounts",
                newName: "Configuration");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Vendors",
                newName: "CodeSTTE");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Vendors",
                newName: "DateStart");

            migrationBuilder.RenameColumn(
                name: "PostCode",
                table: "Vendors",
                newName: "CodePSTL");

            migrationBuilder.RenameColumn(
                name: "Phone2",
                table: "Vendors",
                newName: "TextPHON2");

            migrationBuilder.RenameColumn(
                name: "Phone1",
                table: "Vendors",
                newName: "TextPHON1");

            migrationBuilder.RenameColumn(
                name: "OnHold",
                table: "Vendors",
                newName: "SwHold");

            migrationBuilder.RenameColumn(
                name: "LastMaintenanceDate",
                table: "Vendors",
                newName: "DateLastMN");

            migrationBuilder.RenameColumn(
                name: "CurrencyCode",
                table: "Vendors",
                newName: "CurnCode");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Vendors",
                newName: "CodeCTRY");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Vendors",
                newName: "NameCity");

            migrationBuilder.RenameColumn(
                name: "Address4",
                table: "Vendors",
                newName: "TextSTRE4");

            migrationBuilder.RenameColumn(
                name: "Address3",
                table: "Vendors",
                newName: "TextSTRE3");

            migrationBuilder.RenameColumn(
                name: "Address2",
                table: "Vendors",
                newName: "TextSTRE2");

            migrationBuilder.RenameColumn(
                name: "Address1",
                table: "Vendors",
                newName: "TextSTRE1");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Vendors",
                newName: "SwActv");

            migrationBuilder.RenameColumn(
                name: "TransactionDate",
                table: "Transactions",
                newName: "TransDate");

            migrationBuilder.RenameColumn(
                name: "PostSequence",
                table: "Transactions",
                newName: "PostSeq");

            migrationBuilder.RenameColumn(
                name: "ExchangeRate",
                table: "Transactions",
                newName: "ExchRate");

            migrationBuilder.RenameColumn(
                name: "Configuration",
                table: "GlAccounts",
                newName: "Config");
        }
    }
}
