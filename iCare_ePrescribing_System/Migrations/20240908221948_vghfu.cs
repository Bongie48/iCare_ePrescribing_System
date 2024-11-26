using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iCare_ePrescribing_System.Migrations
{
    public partial class vghfu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedInteraction",
                schema: "Identity",
                columns: table => new
                {
                    MedInteractionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InteractionActiveID = table.Column<int>(type: "int", nullable: false),
                    MedicalConditionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedInteraction", x => x.MedInteractionID);
                    table.ForeignKey(
                        name: "FK_MedInteraction_ActiveIngredients_InteractionActiveID",
                        column: x => x.InteractionActiveID,
                        principalSchema: "Identity",
                        principalTable: "ActiveIngredients",
                        principalColumn: "ActiveID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedInteraction_Condition_MedicalConditionID",
                        column: x => x.MedicalConditionID,
                        principalSchema: "Identity",
                        principalTable: "Condition",
                        principalColumn: "ConditionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedInteraction_InteractionActiveID",
                schema: "Identity",
                table: "MedInteraction",
                column: "InteractionActiveID");

            migrationBuilder.CreateIndex(
                name: "IX_MedInteraction_MedicalConditionID",
                schema: "Identity",
                table: "MedInteraction",
                column: "MedicalConditionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedInteraction",
                schema: "Identity");
        }
    }
}
