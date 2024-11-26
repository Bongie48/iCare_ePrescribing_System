using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iCare_ePrescribing_System.Migrations
{
    public partial class removeextraidfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "itemId",
                schema: "Identity",
                table: "PrescribedMedication");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "itemId",
                schema: "Identity",
                table: "PrescribedMedication",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
