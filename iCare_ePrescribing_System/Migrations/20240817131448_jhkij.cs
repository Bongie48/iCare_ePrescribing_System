using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iCare_ePrescribing_System.Migrations
{
    public partial class jhkij : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DayHospitalId",
                schema: "Identity",
                table: "MedicationOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedicationIngredientMedIngredientId",
                schema: "Identity",
                table: "MedicationIngredients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdminMedIngredientsAdminIngredientId",
                schema: "Identity",
                table: "AdminMedIngredients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicationOrders_DayHospitalId",
                schema: "Identity",
                table: "MedicationOrders",
                column: "DayHospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationIngredients_MedicationIngredientMedIngredientId",
                schema: "Identity",
                table: "MedicationIngredients",
                column: "MedicationIngredientMedIngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminMedIngredients_AdminMedIngredientsAdminIngredientId",
                schema: "Identity",
                table: "AdminMedIngredients",
                column: "AdminMedIngredientsAdminIngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminMedIngredients_AdminMedIngredients_AdminMedIngredientsAdminIngredientId",
                schema: "Identity",
                table: "AdminMedIngredients",
                column: "AdminMedIngredientsAdminIngredientId",
                principalSchema: "Identity",
                principalTable: "AdminMedIngredients",
                principalColumn: "AdminIngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationIngredients_MedicationIngredients_MedicationIngredientMedIngredientId",
                schema: "Identity",
                table: "MedicationIngredients",
                column: "MedicationIngredientMedIngredientId",
                principalSchema: "Identity",
                principalTable: "MedicationIngredients",
                principalColumn: "MedIngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationOrders_DayHospitals_DayHospitalId",
                schema: "Identity",
                table: "MedicationOrders",
                column: "DayHospitalId",
                principalSchema: "Identity",
                principalTable: "DayHospitals",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminMedIngredients_AdminMedIngredients_AdminMedIngredientsAdminIngredientId",
                schema: "Identity",
                table: "AdminMedIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicationIngredients_MedicationIngredients_MedicationIngredientMedIngredientId",
                schema: "Identity",
                table: "MedicationIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicationOrders_DayHospitals_DayHospitalId",
                schema: "Identity",
                table: "MedicationOrders");

            migrationBuilder.DropIndex(
                name: "IX_MedicationOrders_DayHospitalId",
                schema: "Identity",
                table: "MedicationOrders");

            migrationBuilder.DropIndex(
                name: "IX_MedicationIngredients_MedicationIngredientMedIngredientId",
                schema: "Identity",
                table: "MedicationIngredients");

            migrationBuilder.DropIndex(
                name: "IX_AdminMedIngredients_AdminMedIngredientsAdminIngredientId",
                schema: "Identity",
                table: "AdminMedIngredients");

            migrationBuilder.DropColumn(
                name: "DayHospitalId",
                schema: "Identity",
                table: "MedicationOrders");

            migrationBuilder.DropColumn(
                name: "MedicationIngredientMedIngredientId",
                schema: "Identity",
                table: "MedicationIngredients");

            migrationBuilder.DropColumn(
                name: "AdminMedIngredientsAdminIngredientId",
                schema: "Identity",
                table: "AdminMedIngredients");
        }
    }
}
