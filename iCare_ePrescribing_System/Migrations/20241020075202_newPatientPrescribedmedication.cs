using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iCare_ePrescribing_System.Migrations
{
    public partial class newPatientPrescribedmedication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrescribedMedication",
                schema: "Identity",
                columns: table => new
                {
                    PrescribedMedicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    TimeAdministered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuantityAdministered = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescribedMedication", x => x.PrescribedMedicationId);
                    table.ForeignKey(
                        name: "FK_PrescribedMedication_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Identity",
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrescribedMedication_PatientId",
                schema: "Identity",
                table: "PrescribedMedication",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrescribedMedication",
                schema: "Identity");
        }
    }
}
