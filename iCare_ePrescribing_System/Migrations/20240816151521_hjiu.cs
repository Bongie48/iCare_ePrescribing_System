using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iCare_ePrescribing_System.Migrations
{
    public partial class hjiu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "medicineid",
                schema: "Identity",
                table: "Medications");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "medicineid",
                schema: "Identity",
                table: "Medications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
