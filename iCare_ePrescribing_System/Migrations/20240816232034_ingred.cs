using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iCare_ePrescribing_System.Migrations
{
    public partial class ingred : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminMedIngredients_Medications_medicineid",
                schema: "Identity",
                table: "AdminMedIngredients");

            migrationBuilder.DropIndex(
                name: "IX_AdminMedIngredients_medicineid",
                schema: "Identity",
                table: "AdminMedIngredients");

            migrationBuilder.DropColumn(
                name: "medicineid",
                schema: "Identity",
                table: "AdminMedIngredients");

            migrationBuilder.CreateIndex(
                name: "IX_AdminMedIngredients_adminmedicineid",
                schema: "Identity",
                table: "AdminMedIngredients",
                column: "adminmedicineid");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminMedIngredients_Medications_adminmedicineid",
                schema: "Identity",
                table: "AdminMedIngredients",
                column: "adminmedicineid",
                principalSchema: "Identity",
                principalTable: "Medications",
                principalColumn: "MedicationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminMedIngredients_Medications_adminmedicineid",
                schema: "Identity",
                table: "AdminMedIngredients");

            migrationBuilder.DropIndex(
                name: "IX_AdminMedIngredients_adminmedicineid",
                schema: "Identity",
                table: "AdminMedIngredients");

            migrationBuilder.AddColumn<int>(
                name: "medicineid",
                schema: "Identity",
                table: "AdminMedIngredients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AdminMedIngredients_medicineid",
                schema: "Identity",
                table: "AdminMedIngredients",
                column: "medicineid");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminMedIngredients_Medications_medicineid",
                schema: "Identity",
                table: "AdminMedIngredients",
                column: "medicineid",
                principalSchema: "Identity",
                principalTable: "Medications",
                principalColumn: "MedicationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
