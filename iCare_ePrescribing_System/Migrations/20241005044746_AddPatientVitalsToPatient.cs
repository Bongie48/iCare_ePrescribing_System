using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iCare_ePrescribing_System.Migrations
{
    public partial class AddPatientVitalsToPatient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                schema: "Identity",
                table: "PatientVitals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientVitals_PatientId",
                schema: "Identity",
                table: "PatientVitals",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientVitals_Patients_PatientId",
                schema: "Identity",
                table: "PatientVitals",
                column: "PatientId",
                principalSchema: "Identity",
                principalTable: "Patients",
                principalColumn: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientVitals_Patients_PatientId",
                schema: "Identity",
                table: "PatientVitals");

            migrationBuilder.DropIndex(
                name: "IX_PatientVitals_PatientId",
                schema: "Identity",
                table: "PatientVitals");

            migrationBuilder.DropColumn(
                name: "PatientId",
                schema: "Identity",
                table: "PatientVitals");
        }
    }
}
