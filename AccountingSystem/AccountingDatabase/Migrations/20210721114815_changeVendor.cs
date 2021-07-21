using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountingDatabase.Migrations
{
    public partial class changeVendor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VendorName",
                table: "Vendors",
                newName: "VendName");

            migrationBuilder.RenameColumn(
                name: "VendorCode",
                table: "Vendors",
                newName: "VendorID");

            migrationBuilder.AddColumn<string>(
                name: "CodeCTRY",
                table: "Vendors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodePSTL",
                table: "Vendors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeSTTE",
                table: "Vendors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurnCode",
                table: "Vendors",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateLastMN",
                table: "Vendors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateStart",
                table: "Vendors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "IDGRP",
                table: "Vendors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameCity",
                table: "Vendors",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "Vendors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SwActv",
                table: "Vendors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SwHold",
                table: "Vendors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TaxClass1",
                table: "Vendors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TermsCode",
                table: "Vendors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TextPHON1",
                table: "Vendors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TextPHON2",
                table: "Vendors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TextSTRE1",
                table: "Vendors",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TextSTRE2",
                table: "Vendors",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TextSTRE3",
                table: "Vendors",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TextSTRE4",
                table: "Vendors",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeCTRY",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "CodePSTL",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "CodeSTTE",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "CurnCode",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "DateLastMN",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "DateStart",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "IDGRP",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "NameCity",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "SwActv",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "SwHold",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "TaxClass1",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "TermsCode",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "TextPHON1",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "TextPHON2",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "TextSTRE1",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "TextSTRE2",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "TextSTRE3",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "TextSTRE4",
                table: "Vendors");

            migrationBuilder.RenameColumn(
                name: "VendName",
                table: "Vendors",
                newName: "VendorName");

            migrationBuilder.RenameColumn(
                name: "VendorID",
                table: "Vendors",
                newName: "VendorCode");
        }
    }
}
