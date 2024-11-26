using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iCare_ePrescribing_System.Migrations
{
    public partial class fuy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicationOrders_DayHospitals_DayHospitalId",
                schema: "Identity",
                table: "MedicationOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicationOrders_UserStocks_StockUserID",
                schema: "Identity",
                table: "MedicationOrders");

            migrationBuilder.RenameColumn(
                name: "StockUserID",
                schema: "Identity",
                table: "MedicationOrders",
                newName: "StockID");

            migrationBuilder.RenameColumn(
                name: "DayHospitalId",
                schema: "Identity",
                table: "MedicationOrders",
                newName: "UserStockStockID");

            migrationBuilder.RenameIndex(
                name: "IX_MedicationOrders_StockUserID",
                schema: "Identity",
                table: "MedicationOrders",
                newName: "IX_MedicationOrders_StockID");

            migrationBuilder.RenameIndex(
                name: "IX_MedicationOrders_DayHospitalId",
                schema: "Identity",
                table: "MedicationOrders",
                newName: "IX_MedicationOrders_UserStockStockID");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationOrders_DayHospitals_StockID",
                schema: "Identity",
                table: "MedicationOrders",
                column: "StockID",
                principalSchema: "Identity",
                principalTable: "DayHospitals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationOrders_UserStocks_UserStockStockID",
                schema: "Identity",
                table: "MedicationOrders",
                column: "UserStockStockID",
                principalSchema: "Identity",
                principalTable: "UserStocks",
                principalColumn: "StockID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicationOrders_DayHospitals_StockID",
                schema: "Identity",
                table: "MedicationOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicationOrders_UserStocks_UserStockStockID",
                schema: "Identity",
                table: "MedicationOrders");

            migrationBuilder.RenameColumn(
                name: "UserStockStockID",
                schema: "Identity",
                table: "MedicationOrders",
                newName: "DayHospitalId");

            migrationBuilder.RenameColumn(
                name: "StockID",
                schema: "Identity",
                table: "MedicationOrders",
                newName: "StockUserID");

            migrationBuilder.RenameIndex(
                name: "IX_MedicationOrders_UserStockStockID",
                schema: "Identity",
                table: "MedicationOrders",
                newName: "IX_MedicationOrders_DayHospitalId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicationOrders_StockID",
                schema: "Identity",
                table: "MedicationOrders",
                newName: "IX_MedicationOrders_StockUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationOrders_DayHospitals_DayHospitalId",
                schema: "Identity",
                table: "MedicationOrders",
                column: "DayHospitalId",
                principalSchema: "Identity",
                principalTable: "DayHospitals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationOrders_UserStocks_StockUserID",
                schema: "Identity",
                table: "MedicationOrders",
                column: "StockUserID",
                principalSchema: "Identity",
                principalTable: "UserStocks",
                principalColumn: "StockID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
