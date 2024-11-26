using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iCare_ePrescribing_System.Migrations
{
    public partial class addNursetoAdministeredMedication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NurseId",
                schema: "Identity",
                table: "PrescribedMedication",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrescribedMedication_NurseId",
                schema: "Identity",
                table: "PrescribedMedication",
                column: "NurseId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrescribedMedication_Staff_NurseId",
                schema: "Identity",
                table: "PrescribedMedication",
                column: "NurseId",
                principalSchema: "Identity",
                principalTable: "Staff",
                principalColumn: "StaffId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrescribedMedication_Staff_NurseId",
                schema: "Identity",
                table: "PrescribedMedication");

            migrationBuilder.DropIndex(
                name: "IX_PrescribedMedication_NurseId",
                schema: "Identity",
                table: "PrescribedMedication");

            migrationBuilder.DropColumn(
                name: "NurseId",
                schema: "Identity",
                table: "PrescribedMedication");
        }
    }
}
