using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iCare_ePrescribing_System.Migrations
{
    public partial class removedorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                schema: "Identity",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "ReOrder",
                schema: "Identity",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "ReOrderStatus",
                schema: "Identity",
                table: "Medications");

            migrationBuilder.AddColumn<string>(
                name: "InTakeInstructions",
                schema: "Identity",
                table: "PatientMedication",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InTakeInstructions",
                schema: "Identity",
                table: "PatientMedication");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                schema: "Identity",
                table: "Medications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReOrder",
                schema: "Identity",
                table: "Medications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ReOrderStatus",
                schema: "Identity",
                table: "Medications",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");
        }
    }
}
