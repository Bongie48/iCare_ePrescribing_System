using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iCare_ePrescribing_System.Migrations
{
    public partial class AddTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surgeries_Surgeon_SurgeonId",
                schema: "Identity",
                table: "Surgeries");

            migrationBuilder.RenameColumn(
                name: "SurgeonId",
                schema: "Identity",
                table: "Surgeries",
                newName: "SurgeonID");

            migrationBuilder.RenameIndex(
                name: "IX_Surgeries_SurgeonId",
                schema: "Identity",
                table: "Surgeries",
                newName: "IX_Surgeries_SurgeonID");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "Identity",
                table: "Surgeries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Surgeries_Surgeon_SurgeonID",
                schema: "Identity",
                table: "Surgeries",
                column: "SurgeonID",
                principalSchema: "Identity",
                principalTable: "Surgeon",
                principalColumn: "SurgeonId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surgeries_Surgeon_SurgeonID",
                schema: "Identity",
                table: "Surgeries");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "Identity",
                table: "Surgeries");

            migrationBuilder.RenameColumn(
                name: "SurgeonID",
                schema: "Identity",
                table: "Surgeries",
                newName: "SurgeonId");

            migrationBuilder.RenameIndex(
                name: "IX_Surgeries_SurgeonID",
                schema: "Identity",
                table: "Surgeries",
                newName: "IX_Surgeries_SurgeonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Surgeries_Surgeon_SurgeonId",
                schema: "Identity",
                table: "Surgeries",
                column: "SurgeonId",
                principalSchema: "Identity",
                principalTable: "Surgeon",
                principalColumn: "SurgeonId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
