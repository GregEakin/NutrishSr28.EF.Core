using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBSetup.Migrations
{
    /// <inheritdoc />
    public partial class NutData4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NUT_DATA_FoodDescriptionId1",
                table: "NUT_DATA");

            migrationBuilder.CreateIndex(
                name: "IX_NUT_DATA_FoodDescriptionId1",
                table: "NUT_DATA",
                column: "FoodDescriptionId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NUT_DATA_FoodDescriptionId1",
                table: "NUT_DATA");

            migrationBuilder.CreateIndex(
                name: "IX_NUT_DATA_FoodDescriptionId1",
                table: "NUT_DATA",
                column: "FoodDescriptionId1",
                unique: true,
                filter: "[FoodDescriptionId1] IS NOT NULL");
        }
    }
}
