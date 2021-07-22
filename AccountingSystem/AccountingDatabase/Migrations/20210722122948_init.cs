using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountingDatabase.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GlAccounts",
                columns: table => new
                {
                    AccountNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Config = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    In = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlAccounts", x => x.AccountNumber);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GLAccount = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PostSeq = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BatchEntry = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SourceCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    InvoiceNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InvoiceReceiveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    VendorID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VendName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Credit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ExchRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    YearPeriod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionID);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    VendorID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VendName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IDGRP = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SwActv = table.Column<bool>(type: "bit", nullable: false),
                    SwHold = table.Column<bool>(type: "bit", nullable: false),
                    CurnCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    TermsCode = table.Column<int>(type: "int", nullable: false),
                    TaxClass1 = table.Column<int>(type: "int", nullable: false),
                    DateLastMN = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TextSTRE1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TextSTRE2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TextSTRE3 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TextSTRE4 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NameCity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CodeSTTE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodePSTL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodeCTRY = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TextPHON1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TextPHON2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.VendorID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlAccounts");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Vendors");
        }
    }
}
