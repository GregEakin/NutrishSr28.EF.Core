using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBSetup.Migrations
{
    /// <inheritdoc />
    public partial class NutData5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NUT_DATA_FOOD_DES_FoodDescriptionId1",
                table: "NUT_DATA");

            migrationBuilder.DropIndex(
                name: "IX_NUT_DATA_FoodDescriptionId1",
                table: "NUT_DATA");

            migrationBuilder.DropColumn(
                name: "FoodDescriptionId1",
                table: "NUT_DATA");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FoodDescriptionId1",
                table: "NUT_DATA",
                type: "nvarchar(5)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NUT_DATA_FoodDescriptionId1",
                table: "NUT_DATA",
                column: "FoodDescriptionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_NUT_DATA_FOOD_DES_FoodDescriptionId1",
                table: "NUT_DATA",
                column: "FoodDescriptionId1",
                principalTable: "FOOD_DES",
                principalColumn: "NDB_No");
        }
    }
}
