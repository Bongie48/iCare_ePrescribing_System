using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iCare_ePrescribing_System.Migrations
{
    public partial class AddNulableRemainigQuantityFieldtoTrackRemainingMeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RemaininingQuantity",
                schema: "Identity",
                table: "PrescriptionItem",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemaininingQuantity",
                schema: "Identity",
                table: "PrescriptionItem");
        }
    }
}
