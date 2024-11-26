using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iCare_ePrescribing_System.Migrations
{
    public partial class hjfkiyg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                schema: "Identity",
                table: "Staff");

            migrationBuilder.AddColumn<DateTime>(
                name: "RejectDate",
                schema: "Identity",
                table: "RejectPrescription",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RejectDate",
                schema: "Identity",
                table: "RejectPrescription");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                schema: "Identity",
                table: "Staff",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
