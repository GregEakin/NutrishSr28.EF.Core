using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBSetup.Migrations
{
    /// <inheritdoc />
    public partial class FOOD_DESC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FOOD_DES",
                columns: table => new
                {
                    NDB_No = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false, comment: "5-digit Nutrient Databank number that uniquely identifies a food item.  If this field is defined as numeric, the leading zero will be lost."),
                    FdGrp_Cd = table.Column<string>(type: "character varying(4)", nullable: false),
                    Long_Desc = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, comment: "200-character description of food item."),
                    Shrt_Desc = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false, comment: "60-character abbreviated description of food item. Generated from the 200-character description using abbreviations in Appendix A. If short description is longer than 60 characters, additional abbreviations are made. "),
                    ComName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, comment: "Other names commonly used to describe a food, including local or regional names for various foods, for example, 'soda' or 'pop' for 'carbonated beverages.'"),
                    ManufacName = table.Column<string>(type: "character varying(65)", maxLength: 65, nullable: true, comment: "Indicates the company that manufactured the product, when appropriate."),
                    Survey = table.Column<char>(type: "character(1)", nullable: true, comment: "Indicates if the food item is used in the USDA Food and Nutrient Database for Dietary Studies (FNDDS) and thus has a complete nutrient profile for the 65 FNDDS nutrients."),
                    Ref_desc = table.Column<string>(type: "character varying(135)", maxLength: 135, nullable: true, comment: "Description of inedible parts of a food item (refuse), such as seeds or bone."),
                    Refuse = table.Column<decimal>(type: "numeric(2,0)", precision: 2, scale: 0, nullable: true, comment: "Percentage of refuse."),
                    SciName = table.Column<string>(type: "character varying(65)", maxLength: 65, nullable: true, comment: "Scientific name of the food item. Given for the least processed form of the food (usually raw), if applicable."),
                    N_Factor = table.Column<decimal>(type: "numeric(4,2)", precision: 4, scale: 2, nullable: true, comment: "Factor for converting nitrogen to protein."),
                    Pro_Factor = table.Column<decimal>(type: "numeric(4,2)", precision: 4, scale: 2, nullable: true, comment: "Factor for calculating calories from protein."),
                    Fat_Factor = table.Column<decimal>(type: "numeric(4,2)", precision: 4, scale: 2, nullable: true, comment: "Factor for calculating calories from fat."),
                    CHO_Factor = table.Column<decimal>(type: "numeric(4,2)", precision: 4, scale: 2, nullable: true, comment: "Factor for calculating calories from carbohydrate.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FOOD_DES", x => x.NDB_No);
                    table.ForeignKey(
                        name: "FK_FOOD_DES_FD_GROUP_FdGrp_Cd",
                        column: x => x.FdGrp_Cd,
                        principalTable: "FD_GROUP",
                        principalColumn: "FdGrp_Cd",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "This file contains long and short descriptions and food group designators for all food items, along with common names, manufacturer name, scientific name, percentage and description of refuse, and factors used for calculating protein and kilocalories, if applicable. Items used in the FNDDS are also identified by value of 'Y' in the Survey field. ");

            migrationBuilder.CreateIndex(
                name: "IX_FOOD_DES_FdGrp_Cd",
                table: "FOOD_DES",
                column: "FdGrp_Cd");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FOOD_DES");
        }
    }
}
