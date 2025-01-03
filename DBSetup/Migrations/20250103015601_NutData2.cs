using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBSetup.Migrations
{
    /// <inheritdoc />
    public partial class NutData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FOOTNOTE_NUT_DATA_NutrientDataFoodDescriptionId_NutrientDataNutrientDefinitionId",
                table: "FOOTNOTE");

            migrationBuilder.DropIndex(
                name: "IX_FOOTNOTE_NutrientDataFoodDescriptionId_NutrientDataNutrientDefinitionId",
                table: "FOOTNOTE");

            migrationBuilder.DropColumn(
                name: "NutrientDataFoodDescriptionId",
                table: "FOOTNOTE");

            migrationBuilder.DropColumn(
                name: "NutrientDataNutrientDefinitionId",
                table: "FOOTNOTE");

            migrationBuilder.CreateTable(
                name: "FootnoteNutrientData",
                columns: table => new
                {
                    FootnotesFootnoteId = table.Column<int>(type: "int", nullable: false),
                    NutrientDataFoodDescriptionId = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    NutrientDataNutrientDefinitionId = table.Column<string>(type: "nvarchar(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FootnoteNutrientData", x => new { x.FootnotesFootnoteId, x.NutrientDataFoodDescriptionId, x.NutrientDataNutrientDefinitionId });
                    table.ForeignKey(
                        name: "FK_FootnoteNutrientData_FOOTNOTE_FootnotesFootnoteId",
                        column: x => x.FootnotesFootnoteId,
                        principalTable: "FOOTNOTE",
                        principalColumn: "FootnoteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FootnoteNutrientData_NUT_DATA_NutrientDataFoodDescriptionId_NutrientDataNutrientDefinitionId",
                        columns: x => new { x.NutrientDataFoodDescriptionId, x.NutrientDataNutrientDefinitionId },
                        principalTable: "NUT_DATA",
                        principalColumns: new[] { "NDB_No", "Nutr_No" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FootnoteNutrientData_NutrientDataFoodDescriptionId_NutrientDataNutrientDefinitionId",
                table: "FootnoteNutrientData",
                columns: new[] { "NutrientDataFoodDescriptionId", "NutrientDataNutrientDefinitionId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FootnoteNutrientData");

            migrationBuilder.AddColumn<string>(
                name: "NutrientDataFoodDescriptionId",
                table: "FOOTNOTE",
                type: "nvarchar(5)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NutrientDataNutrientDefinitionId",
                table: "FOOTNOTE",
                type: "nvarchar(3)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FOOTNOTE_NutrientDataFoodDescriptionId_NutrientDataNutrientDefinitionId",
                table: "FOOTNOTE",
                columns: new[] { "NutrientDataFoodDescriptionId", "NutrientDataNutrientDefinitionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FOOTNOTE_NUT_DATA_NutrientDataFoodDescriptionId_NutrientDataNutrientDefinitionId",
                table: "FOOTNOTE",
                columns: new[] { "NutrientDataFoodDescriptionId", "NutrientDataNutrientDefinitionId" },
                principalTable: "NUT_DATA",
                principalColumns: new[] { "NDB_No", "Nutr_No" });
        }
    }
}
