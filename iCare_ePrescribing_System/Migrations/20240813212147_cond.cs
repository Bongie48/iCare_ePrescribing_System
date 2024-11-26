using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iCare_ePrescribing_System.Migrations
{
    public partial class cond : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActiveIngredientID",
                schema: "Identity",
                table: "Medications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "medicineid",
                schema: "Identity",
                table: "Medications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConditionCode",
                schema: "Identity",
                table: "Condition",
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medications_ActiveIngredients_ActiveIngredientID",
                schema: "Identity",
                table: "Medications");

            migrationBuilder.DropIndex(
                name: "IX_Medications_ActiveIngredientID",
                schema: "Identity",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "ActiveIngredientID",
                schema: "Identity",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "medicineid",
                schema: "Identity",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "ConditionCode",
                schema: "Identity",
                table: "Condition");
        }
    }
}
