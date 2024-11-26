using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iCare_ePrescribing_System.Migrations
{
    public partial class addHomeAddressFieldToPatient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HomeAddress",
                schema: "Identity",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HomeAddress",
                schema: "Identity",
                table: "Patients");
        }
    }
}
