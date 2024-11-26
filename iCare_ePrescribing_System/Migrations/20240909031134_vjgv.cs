using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iCare_ePrescribing_System.Migrations
{
    public partial class vjgv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedContra",
                schema: "Identity",
                columns: table => new
                {
                    MedInterationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentActiveID = table.Column<int>(type: "int", nullable: false),
                    InteratingActiveID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedContra", x => x.MedInterationID);
                    table.ForeignKey(
                        name: "FK_MedContra_ActiveIngredients_CurrentActiveID",
                        column: x => x.CurrentActiveID,
                        principalSchema: "Identity",
                        principalTable: "ActiveIngredients",
                        principalColumn: "ActiveID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedContra_ActiveIngredients_InteratingActiveID",
                        column: x => x.InteratingActiveID,
                        principalSchema: "Identity",
                        principalTable: "ActiveIngredients",
                        principalColumn: "ActiveID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedContra_CurrentActiveID",
                schema: "Identity",
                table: "MedContra",
                column: "CurrentActiveID");

            migrationBuilder.CreateIndex(
                name: "IX_MedContra_InteratingActiveID",
                schema: "Identity",
                table: "MedContra",
                column: "InteratingActiveID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedContra",
                schema: "Identity");
        }
    }
}
