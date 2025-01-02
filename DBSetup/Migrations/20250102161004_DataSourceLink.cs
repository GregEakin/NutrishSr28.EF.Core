using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBSetup.Migrations
{
    /// <inheritdoc />
    public partial class DataSourceLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DATASRCLN",
                columns: table => new
                {
                    NDB_No = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false, comment: "5-digit Nutrient Databank number that uniquely identifies a food item. If this field is defined as numeric, the leading zero will be lost."),
                    Nutr_No = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false, comment: "Unique 3-digit identifier code for a nutrient."),
                    DataSrc_ID = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false, comment: "Unique ID identifying the reference/source.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DATASRCLN", x => new { x.NDB_No, x.Nutr_No, x.DataSrc_ID });
                    table.ForeignKey(
                        name: "FK_DATASRCLN_DATA_SRC_DataSrc_ID",
                        column: x => x.DataSrc_ID,
                        principalTable: "DATA_SRC",
                        principalColumn: "DataSrc_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DATASRCLN_FOOD_DES_NDB_No",
                        column: x => x.NDB_No,
                        principalTable: "FOOD_DES",
                        principalColumn: "NDB_No",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DATASRCLN_NUTR_DEF_Nutr_No",
                        column: x => x.Nutr_No,
                        principalTable: "NUTR_DEF",
                        principalColumn: "Nutr_No",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "This file is used to link the Nutrient Data file with the Sources of Data table. It is needed to resolve the many-to-many relationship between the two tables.");

            migrationBuilder.CreateIndex(
                name: "IX_DATASRCLN_DataSrc_ID",
                table: "DATASRCLN",
                column: "DataSrc_ID");

            migrationBuilder.CreateIndex(
                name: "IX_DATASRCLN_Nutr_No",
                table: "DATASRCLN",
                column: "Nutr_No");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DATASRCLN");
        }
    }
}
