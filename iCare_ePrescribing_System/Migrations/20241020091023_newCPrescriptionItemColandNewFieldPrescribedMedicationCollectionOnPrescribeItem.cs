using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iCare_ePrescribing_System.Migrations
{
    public partial class newCPrescriptionItemColandNewFieldPrescribedMedicationCollectionOnPrescribeItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrescriptionItemId",
                schema: "Identity",
                table: "PrescribedMedication",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "itemId",
                schema: "Identity",
                table: "PrescribedMedication",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PrescribedMedication_PrescriptionItemId",
                schema: "Identity",
                table: "PrescribedMedication",
                column: "PrescriptionItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrescribedMedication_PrescriptionItem_PrescriptionItemId",
                schema: "Identity",
                table: "PrescribedMedication",
                column: "PrescriptionItemId",
                principalSchema: "Identity",
                principalTable: "PrescriptionItem",
                principalColumn: "itemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrescribedMedication_PrescriptionItem_PrescriptionItemId",
                schema: "Identity",
                table: "PrescribedMedication");

            migrationBuilder.DropIndex(
                name: "IX_PrescribedMedication_PrescriptionItemId",
                schema: "Identity",
                table: "PrescribedMedication");

            migrationBuilder.DropColumn(
                name: "PrescriptionItemId",
                schema: "Identity",
                table: "PrescribedMedication");

            migrationBuilder.DropColumn(
                name: "itemId",
                schema: "Identity",
                table: "PrescribedMedication");
        }
    }
}
