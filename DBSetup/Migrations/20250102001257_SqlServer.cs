using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBSetup.Migrations
{
    /// <inheritdoc />
    public partial class SqlServer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FD_GROUP",
                columns: table => new
                {
                    FdGrp_Cd = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false, comment: "4-digit code identifying a food group. Only the first 2 ndigits are currently assigned. In the future, the last 2 digits may be used. Codes may not be consecutive."),
                    FdGrp_Desc = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false, comment: "Name of food group.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FD_GROUP", x => x.FdGrp_Cd);
                },
                comment: "Contains a list of food groups used in SR28 and their descriptions.");

            migrationBuilder.CreateTable(
                name: "LANGDESC",
                columns: table => new
                {
                    Factor_Code = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false, comment: "The LanguaL factor from the Thesaurus. Only those codes used to factor the foods contained in the LanguaL Factor file are included in this file. "),
                    Description = table.Column<string>(type: "nvarchar(140)", maxLength: 140, nullable: false, comment: "The description of the LanguaL Factor Code from the thesaurus. ")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LANGDESC", x => x.Factor_Code);
                },
                comment: "This file is a support file to the LanguaL Factor file and contains the descriptions for only those factors used in coding the selected food items codes in this release of SR.");

            migrationBuilder.CreateTable(
                name: "FOOD_DES",
                columns: table => new
                {
                    NDB_No = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false, comment: "5-digit Nutrient Databank number that uniquely identifies a food item.  If this field is defined as numeric, the leading zero will be lost."),
                    FdGrp_Cd = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false, comment: "4-digit code indicating food group to which a food item belongs."),
                    Long_Desc = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "200-character description of food item."),
                    Shrt_Desc = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false, comment: "60-character abbreviated description of food item. Generated from the 200-character description using abbreviations in Appendix A. If short description is longer than 60 characters, additional abbreviations are made. "),
                    ComName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "Other names commonly used to describe a food, including local or regional names for various foods, for example, 'soda' or 'pop' for 'carbonated beverages.'"),
                    ManufacName = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: true, comment: "Indicates the company that manufactured the product, when appropriate."),
                    Survey = table.Column<string>(type: "nvarchar(1)", nullable: true, comment: "Indicates if the food item is used in the USDA Food and Nutrient Database for Dietary Studies (FNDDS) and thus has a complete nutrient profile for the 65 FNDDS nutrients."),
                    Ref_desc = table.Column<string>(type: "nvarchar(135)", maxLength: 135, nullable: true, comment: "Description of inedible parts of a food item (refuse), such as seeds or bone."),
                    Refuse = table.Column<decimal>(type: "decimal(2,0)", precision: 2, scale: 0, nullable: true, comment: "Percentage of refuse."),
                    SciName = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: true, comment: "Scientific name of the food item. Given for the least processed form of the food (usually raw), if applicable."),
                    N_Factor = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: true, comment: "Factor for converting nitrogen to protein."),
                    Pro_Factor = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: true, comment: "Factor for calculating calories from protein."),
                    Fat_Factor = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: true, comment: "Factor for calculating calories from fat."),
                    CHO_Factor = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: true, comment: "Factor for calculating calories from carbohydrate.")
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

            migrationBuilder.CreateTable(
                name: "LANGUAL",
                columns: table => new
                {
                    NDB_No = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false, comment: "5-digit Nutrient Databank number that uniquely identifies a food item. If this field is defined as numeric, the leading zero will be lost."),
                    Factor_Code = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false, comment: "The LanguaL factor from the Thesaurus.")
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
                name: "IX_FOOD_DES_FdGrp_Cd",
                table: "FOOD_DES",
                column: "FdGrp_Cd");

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

            migrationBuilder.DropTable(
                name: "FOOD_DES");

            migrationBuilder.DropTable(
                name: "LANGDESC");

            migrationBuilder.DropTable(
                name: "FD_GROUP");
        }
    }
}
