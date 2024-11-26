using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iCare_ePrescribing_System.Migrations
{
    public partial class medicine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medications_ActiveIngredients_ActiveIngredientID",
                schema: "Identity",
                table: "Medications");

            migrationBuilder.DropForeignKey(
                name: "FK_Medications_DosageForm_DosageFormDosageID",
                schema: "Identity",
                table: "Medications");

            migrationBuilder.DropIndex(
                name: "IX_Medications_ActiveIngredientID",
                schema: "Identity",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "Instructions",
                schema: "Identity",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "Strength",
                schema: "Identity",
                table: "Medications");

            migrationBuilder.RenameColumn(
                name: "DosageID",
                schema: "Identity",
                table: "Medications",
                newName: "dosageID");

            migrationBuilder.RenameColumn(
                name: "DosageFormDosageID",
                schema: "Identity",
                table: "Medications",
                newName: "scheduleID");

            migrationBuilder.RenameColumn(
                name: "ActiveIngredientID",
                schema: "Identity",
                table: "Medications",
                newName: "ReOrder");

            migrationBuilder.RenameIndex(
                name: "IX_Medications_DosageFormDosageID",
                schema: "Identity",
                table: "Medications",
                newName: "IX_Medications_scheduleID");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
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

            migrationBuilder.CreateTable(
                name: "AdminMedIngredients",
                schema: "Identity",
                columns: table => new
                {
                    AdminIngredientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Medactiveid = table.Column<int>(type: "int", nullable: false),
                    adminmedicineid = table.Column<int>(type: "int", nullable: false),
                    medicineid = table.Column<int>(type: "int", nullable: false),
                    Strength = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminMedIngredients", x => x.AdminIngredientId);
                    table.ForeignKey(
                        name: "FK_AdminMedIngredients_ActiveIngredients_Medactiveid",
                        column: x => x.Medactiveid,
                        principalSchema: "Identity",
                        principalTable: "ActiveIngredients",
                        principalColumn: "ActiveID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdminMedIngredients_Medications_medicineid",
                        column: x => x.medicineid,
                        principalSchema: "Identity",
                        principalTable: "Medications",
                        principalColumn: "MedicationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medications_dosageID",
                schema: "Identity",
                table: "Medications",
                column: "dosageID");

            migrationBuilder.CreateIndex(
                name: "IX_AdminMedIngredients_Medactiveid",
                schema: "Identity",
                table: "AdminMedIngredients",
                column: "Medactiveid");

            migrationBuilder.CreateIndex(
                name: "IX_AdminMedIngredients_medicineid",
                schema: "Identity",
                table: "AdminMedIngredients",
                column: "medicineid");

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_DosageForm_dosageID",
                schema: "Identity",
                table: "Medications",
                column: "dosageID",
                principalSchema: "Identity",
                principalTable: "DosageForm",
                principalColumn: "DosageID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_Schedule_scheduleID",
                schema: "Identity",
                table: "Medications",
                column: "scheduleID",
                principalSchema: "Identity",
                principalTable: "Schedule",
                principalColumn: "ScheduleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medications_DosageForm_dosageID",
                schema: "Identity",
                table: "Medications");

            migrationBuilder.DropForeignKey(
                name: "FK_Medications_Schedule_scheduleID",
                schema: "Identity",
                table: "Medications");

            migrationBuilder.DropTable(
                name: "AdminMedIngredients",
                schema: "Identity");

            migrationBuilder.DropIndex(
                name: "IX_Medications_dosageID",
                schema: "Identity",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "Quantity",
                schema: "Identity",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "ReOrderStatus",
                schema: "Identity",
                table: "Medications");

            migrationBuilder.RenameColumn(
                name: "dosageID",
                schema: "Identity",
                table: "Medications",
                newName: "DosageID");

            migrationBuilder.RenameColumn(
                name: "scheduleID",
                schema: "Identity",
                table: "Medications",
                newName: "DosageFormDosageID");

            migrationBuilder.RenameColumn(
                name: "ReOrder",
                schema: "Identity",
                table: "Medications",
                newName: "ActiveIngredientID");

            migrationBuilder.RenameIndex(
                name: "IX_Medications_scheduleID",
                schema: "Identity",
                table: "Medications",
                newName: "IX_Medications_DosageFormDosageID");

            migrationBuilder.AddColumn<string>(
                name: "Instructions",
                schema: "Identity",
                table: "Medications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Strength",
                schema: "Identity",
                table: "Medications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Medications_ActiveIngredientID",
                schema: "Identity",
                table: "Medications",
                column: "ActiveIngredientID");

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_ActiveIngredients_ActiveIngredientID",
                schema: "Identity",
                table: "Medications",
                column: "ActiveIngredientID",
                principalSchema: "Identity",
                principalTable: "ActiveIngredients",
                principalColumn: "ActiveID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_DosageForm_DosageFormDosageID",
                schema: "Identity",
                table: "Medications",
                column: "DosageFormDosageID",
                principalSchema: "Identity",
                principalTable: "DosageForm",
                principalColumn: "DosageID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
