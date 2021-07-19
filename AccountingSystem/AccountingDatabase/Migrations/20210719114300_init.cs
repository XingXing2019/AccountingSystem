using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountingDatabase.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gls",
                columns: table => new
                {
                    GLCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GLDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Configuration = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    In = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gls", x => x.GLCode);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatchEntry = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SourceCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    VendorCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GLCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PostingSeq = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InvoiceNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Credit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DocDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvoiceReceiveDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionID);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    VendorCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VendorName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.VendorCode);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gls");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Vendors");
        }
    }
}
