using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBSetup.Migrations
{
    /// <inheritdoc />
    public partial class LANGUAL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LANGUAL",
                columns: table => new
                {
                    NDB_No = table.Column<string>(type: "character varying(5)", nullable: false, comment: "5-digit Nutrient Databank number that uniquely identifies a food item. If this field is defined as numeric, the leading zero will be lost."),
                    Factor_Code = table.Column<string>(type: "character varying(5)", nullable: false, comment: "The LanguaL factor from the Thesaurus.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LANGUAL", x => new { x.NDB_No, x.Factor_Code });
                    table.ForeignKey(
                        name: "FK_LANGUAL_FOOD_DES_NDB_No",
                        column: x => x.NDB_No,
                        principalTable: "FOOD_DES",
                        principalColumn: "NDB_No",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LANGUAL_LANGDESC_Factor_Code",
                        column: x => x.Factor_Code,
                        principalTable: "LANGDESC",
                        principalColumn: "Factor_Code",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "This file is a support file to the Food Description file and contains the factors from the LanguaL Thesaurus used to code a particular food.");

            migrationBuilder.CreateIndex(
                name: "IX_LANGUAL_Factor_Code",
                table: "LANGUAL",
                column: "Factor_Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LANGUAL");

            migrationBuilder.AlterTable(
                name: "LANGDESC",
                comment: " This file is a support file to the LanguaL Factor file and contains the descriptions for only those factors used in coding the selected food items codes in this release of SR.",
                oldComment: "This file is a support file to the LanguaL Factor file and contains the descriptions for only those factors used in coding the selected food items codes in this release of SR.");
        }
    }
}
