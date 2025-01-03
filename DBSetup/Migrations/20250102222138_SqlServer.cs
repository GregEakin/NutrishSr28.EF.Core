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
                name: "DERIVCD",
                columns: table => new
                {
                    Deriv_Cd = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false, comment: "Derivation Code."),
                    Deriv_Desc = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false, comment: "Description of derivation code giving specific information on how the value was determined.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DERIVCD", x => x.Deriv_Cd);
                },
                comment: "This file provides information on how the nutrient values were determined. The file contains the derivation codes and their descriptions.");

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
                name: "NUTR_DEF",
                columns: table => new
                {
                    Nutr_No = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false, comment: "Unique 3-digit identifier code for a nutrient."),
                    Units = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false, comment: "Units of measure (mg, g, μg, and so on)."),
                    Tagname = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "International Network of Food Data Systems (INFOODS) Tagnames. A unique abbreviation for a nutrient/food component developed by INFOODS to aid in the interchange of data."),
                    NutrDesc = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false, comment: "Name of nutrient/food component."),
                    Num_Dec = table.Column<string>(type: "nvarchar(1)", nullable: false, comment: "Number of decimal places to which a nutrient value is rounded."),
                    SR_Order = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false, comment: "Used to sort nutrient records in the same order as \r\nvarious reports produced from SR.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NUTR_DEF", x => x.Nutr_No);
                },
                comment: "This file is a support file to the Nutrient Data file. It provides the 3-digit nutrient code, unit of measure, INFOODS tagname, and description.");

            migrationBuilder.CreateTable(
                name: "SRC",
                columns: table => new
                {
                    Src_Cd = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false, comment: "A 2-digit code indicating type of data."),
                    SrcCd_Desc = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false, comment: "Description of source code that identifies the type of nutrient data.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SRC", x => x.Src_Cd);
                },
                comment: "This file contains codes indicating the type of data (analytical, calculated, assumed zero, and so on) in the Nutrient Data file. To improve the usability of the database and to provide values for the FNDDS, NDL staff imputed nutrient values for a number of proximate components, total dietary fiber, total sugar, and vitamin and mineral values.");

            migrationBuilder.CreateTable(
                name: "FOOD_DES",
                columns: table => new
                {
                    NDB_No = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false, comment: "5-digit Nutrient Databank number that uniquely identifies a food item. If this field is defined as numeric, the leading zero will be lost."),
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

            migrationBuilder.CreateTable(
                name: "NUT_DATA",
                columns: table => new
                {
                    NDB_No = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false, comment: "5-digit Nutrient Databank number that uniquely identifies a food item.  If this field is defined as numeric, the leading zero will be lost."),
                    Nutr_No = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false, comment: "Unique 3-digit identifier code for a nutrient."),
                    Nutr_Val = table.Column<decimal>(type: "decimal(10,3)", precision: 10, scale: 3, nullable: false, comment: "Amount in 100 grams, edible portion."),
                    Num_Data_Pts = table.Column<decimal>(type: "decimal(5,0)", precision: 5, scale: 0, nullable: false, comment: "Number of data points is the number of analyses used to calculate the nutrient value. If the number of data points is 0, the value was calculated or imputed."),
                    Std_Error = table.Column<decimal>(type: "decimal(8,3)", precision: 8, scale: 3, nullable: true, comment: "Standard error of the mean. Null if cannot be calculated. The standard error is also not given if the number of data points is less than three."),
                    Src_Cd = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false, comment: "Code indicating type of data."),
                    Deriv_Cd = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true, comment: "Data Derivation Code giving specific information on how the value is determined. This field is populated only for items added or updated starting with SR14. This field may not be populated if older records were used in the calculation of the mean value."),
                    Ref_NDB_No = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true, comment: "NDB number of the item used to calculate a missing value. Populated only for items added or updated starting with SR14."),
                    Add_Nutr_Mark = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true, comment: "Indicates a vitamin or mineral added for fortification or enrichment. This field is populated for ready-to eat breakfast cereals and many brand-name hot cereals in food group 08."),
                    Num_Studies = table.Column<decimal>(type: "decimal(2,0)", precision: 2, scale: 0, nullable: true, comment: "Number of studies."),
                    Min = table.Column<decimal>(type: "decimal(10,3)", precision: 10, scale: 3, nullable: true, comment: "Minimum value."),
                    Max = table.Column<decimal>(type: "decimal(10,3)", precision: 10, scale: 3, nullable: true, comment: "Maximum value."),
                    DF = table.Column<decimal>(type: "decimal(4,0)", precision: 4, scale: 0, nullable: true, comment: "Degrees of freedom."),
                    Low_EB = table.Column<decimal>(type: "decimal(10,3)", precision: 10, scale: 3, nullable: true, comment: "Lower 95% error bound."),
                    Up_EB = table.Column<decimal>(type: "decimal(10,3)", precision: 10, scale: 3, nullable: true, comment: "Upper 95% error bound."),
                    Stat_cmt = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, comment: "Statistical comments."),
                    AddMod_Date = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, comment: "Indicates when a value was either added to the database or last modified."),
                    CC = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true, comment: "Confidence Code indicating data quality, based on evaluation of sample plan, sample handling, analytical method, analytical quality control, and number of samples analysed. Not included in this release, but is planned for future releases."),
                    FoodDescriptionId1 = table.Column<string>(type: "nvarchar(5)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NUT_DATA", x => new { x.NDB_No, x.Nutr_No });
                    table.ForeignKey(
                        name: "FK_NUT_DATA_DERIVCD_Deriv_Cd",
                        column: x => x.Deriv_Cd,
                        principalTable: "DERIVCD",
                        principalColumn: "Deriv_Cd");
                    table.ForeignKey(
                        name: "FK_NUT_DATA_FOOD_DES_FoodDescriptionId1",
                        column: x => x.FoodDescriptionId1,
                        principalTable: "FOOD_DES",
                        principalColumn: "NDB_No");
                    table.ForeignKey(
                        name: "FK_NUT_DATA_FOOD_DES_NDB_No",
                        column: x => x.NDB_No,
                        principalTable: "FOOD_DES",
                        principalColumn: "NDB_No",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NUT_DATA_FOOD_DES_Ref_NDB_No",
                        column: x => x.Ref_NDB_No,
                        principalTable: "FOOD_DES",
                        principalColumn: "NDB_No",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NUT_DATA_NUTR_DEF_Nutr_No",
                        column: x => x.Nutr_No,
                        principalTable: "NUTR_DEF",
                        principalColumn: "Nutr_No",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NUT_DATA_SRC_Src_Cd",
                        column: x => x.Src_Cd,
                        principalTable: "SRC",
                        principalColumn: "Src_Cd",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "This file contains the nutrient values and information about the values, including expanded statistical information.");

            migrationBuilder.CreateTable(
                name: "DATA_SRC",
                columns: table => new
                {
                    DataSrc_ID = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false, comment: "Unique ID identifying the reference/source."),
                    Authors = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "List of authors for a journal article or name of sponsoring organization for other documents."),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Title of article or name of document, such as a report from a company or trade association."),
                    Year = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true, comment: "Year article or document was published."),
                    Journal = table.Column<string>(type: "nvarchar(135)", maxLength: 135, nullable: true, comment: "Name of the journal in which the article was published."),
                    Vol_City = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true, comment: "Volume number for journal articles, books, or reports; city where sponsoring organization is located."),
                    Issue_State = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true, comment: "Issue number for journal article; State where the sponsoring organization is located."),
                    Start_Page = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true, comment: "Starting page number of article/document."),
                    End_Page = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true, comment: "Ending page number of article/document."),
                    NutrientDataFoodDescriptionId = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    NutrientDataNutrientDefinitionId = table.Column<string>(type: "nvarchar(3)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DATA_SRC", x => x.DataSrc_ID);
                    table.ForeignKey(
                        name: "FK_DATA_SRC_NUT_DATA_NutrientDataFoodDescriptionId_NutrientDataNutrientDefinitionId",
                        columns: x => new { x.NutrientDataFoodDescriptionId, x.NutrientDataNutrientDefinitionId },
                        principalTable: "NUT_DATA",
                        principalColumns: new[] { "NDB_No", "Nutr_No" });
                },
                comment: "This file provides a citation to the DataSrc_ID in the Sources of Data Link file.");

            migrationBuilder.CreateTable(
                name: "FOOTNOTE",
                columns: table => new
                {
                    FootnoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NDB_No = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false, comment: "5-digit Nutrient Databank number that uniquely identifies a food item. If this field is defined as numeric, the leading zero will be lost."),
                    Footnt_No = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false, comment: "Sequence number. If a given footnote applies to more than one nutrient number, the same footnote number is used. As a result, this file cannot be indexed and there is no primary key. "),
                    Footnt_Typ = table.Column<string>(type: "nvarchar(1)", nullable: false, comment: "Type of footnote: D = footnote adding information to the food description;  M = footnote adding information to measure description;  N = footnote providing additional information on a nutrient value. If the Footnt_typ = N, the Nutr_No will also be filled in."),
                    Nutr_No = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true, comment: "Unique 3-digit identifier code for a nutrient to which footnote applies."),
                    Footnt_Txt = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "Footnote text."),
                    NutrientDataFoodDescriptionId = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    NutrientDataNutrientDefinitionId = table.Column<string>(type: "nvarchar(3)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FOOTNOTE", x => x.FootnoteId);
                    table.ForeignKey(
                        name: "FK_FOOTNOTE_FOOD_DES_NDB_No",
                        column: x => x.NDB_No,
                        principalTable: "FOOD_DES",
                        principalColumn: "NDB_No",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FOOTNOTE_NUTR_DEF_Nutr_No",
                        column: x => x.Nutr_No,
                        principalTable: "NUTR_DEF",
                        principalColumn: "Nutr_No");
                    table.ForeignKey(
                        name: "FK_FOOTNOTE_NUT_DATA_NutrientDataFoodDescriptionId_NutrientDataNutrientDefinitionId",
                        columns: x => new { x.NutrientDataFoodDescriptionId, x.NutrientDataNutrientDefinitionId },
                        principalTable: "NUT_DATA",
                        principalColumns: new[] { "NDB_No", "Nutr_No" });
                },
                comment: "This file contains additional information about the food item, household weight, and nutrient value.");

            migrationBuilder.CreateTable(
                name: "WEIGHT",
                columns: table => new
                {
                    NDB_No = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false, comment: "5-digit Nutrient Databank number that uniquely identifies a food item.  If this field is defined as numeric, the leading zero will be lost."),
                    Seq = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false, comment: "Sequence number."),
                    Amount = table.Column<decimal>(type: "decimal(6,3)", precision: 6, scale: 3, nullable: false, comment: "Unit modifier (for example, 1 in '1 cup')."),
                    Msre_Desc = table.Column<string>(type: "nvarchar(84)", maxLength: 84, nullable: false, comment: "Description (for example, cup, diced, and 1-inch pieces)"),
                    Gm_Wgt = table.Column<decimal>(type: "decimal(7,1)", precision: 7, scale: 1, nullable: false, comment: "Gram weight."),
                    Num_Data_Pts = table.Column<decimal>(type: "decimal(4,0)", precision: 4, scale: 0, nullable: true, comment: "Number of data points."),
                    Std_Dev = table.Column<decimal>(type: "decimal(7,3)", precision: 7, scale: 3, nullable: true, comment: "Standard deviation."),
                    NutrientDataFoodDescriptionId = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    NutrientDataNutrientDefinitionId = table.Column<string>(type: "nvarchar(3)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WEIGHT", x => new { x.NDB_No, x.Seq });
                    table.ForeignKey(
                        name: "FK_WEIGHT_FOOD_DES_NDB_No",
                        column: x => x.NDB_No,
                        principalTable: "FOOD_DES",
                        principalColumn: "NDB_No",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WEIGHT_NUT_DATA_NutrientDataFoodDescriptionId_NutrientDataNutrientDefinitionId",
                        columns: x => new { x.NutrientDataFoodDescriptionId, x.NutrientDataNutrientDefinitionId },
                        principalTable: "NUT_DATA",
                        principalColumns: new[] { "NDB_No", "Nutr_No" });
                },
                comment: "This file contains codes indicating the type of data (analytical, calculated, assumed zero, and so on) in the Nutrient Data file. To improve the usability of the database and to provide values for the FNDDS, NDL staff imputed nutrient values for a number of proximate components, total dietary fiber, total sugar, and vitamin and mineral values.");

            migrationBuilder.CreateTable(
                name: "DATSRCLN",
                columns: table => new
                {
                    NDB_No = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false, comment: "5-digit Nutrient Databank number that uniquely identifies a food item. If this field is defined as numeric, the leading zero will be lost."),
                    Nutr_No = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false, comment: "Unique 3-digit identifier code for a nutrient."),
                    DataSrc_ID = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false, comment: "Unique ID identifying the reference/source.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DATSRCLN", x => new { x.NDB_No, x.Nutr_No, x.DataSrc_ID });
                    table.ForeignKey(
                        name: "FK_DATSRCLN_DATA_SRC_DataSrc_ID",
                        column: x => x.DataSrc_ID,
                        principalTable: "DATA_SRC",
                        principalColumn: "DataSrc_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DATSRCLN_FOOD_DES_NDB_No",
                        column: x => x.NDB_No,
                        principalTable: "FOOD_DES",
                        principalColumn: "NDB_No",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DATSRCLN_NUTR_DEF_Nutr_No",
                        column: x => x.Nutr_No,
                        principalTable: "NUTR_DEF",
                        principalColumn: "Nutr_No",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "This file is used to link the Nutrient Data file with the Sources of Data table. It is needed to resolve the many-to-many relationship between the two tables.");

            migrationBuilder.CreateIndex(
                name: "IX_DATA_SRC_NutrientDataFoodDescriptionId_NutrientDataNutrientDefinitionId",
                table: "DATA_SRC",
                columns: new[] { "NutrientDataFoodDescriptionId", "NutrientDataNutrientDefinitionId" });

            migrationBuilder.CreateIndex(
                name: "IX_DATSRCLN_DataSrc_ID",
                table: "DATSRCLN",
                column: "DataSrc_ID");

            migrationBuilder.CreateIndex(
                name: "IX_DATSRCLN_Nutr_No",
                table: "DATSRCLN",
                column: "Nutr_No");

            migrationBuilder.CreateIndex(
                name: "IX_FOOD_DES_FdGrp_Cd",
                table: "FOOD_DES",
                column: "FdGrp_Cd");

            migrationBuilder.CreateIndex(
                name: "IX_FOOTNOTE_NDB_No",
                table: "FOOTNOTE",
                column: "NDB_No");

            migrationBuilder.CreateIndex(
                name: "IX_FOOTNOTE_Nutr_No",
                table: "FOOTNOTE",
                column: "Nutr_No");

            migrationBuilder.CreateIndex(
                name: "IX_FOOTNOTE_NutrientDataFoodDescriptionId_NutrientDataNutrientDefinitionId",
                table: "FOOTNOTE",
                columns: new[] { "NutrientDataFoodDescriptionId", "NutrientDataNutrientDefinitionId" });

            migrationBuilder.CreateIndex(
                name: "IX_LANGUAL_Factor_Code",
                table: "LANGUAL",
                column: "Factor_Code");

            migrationBuilder.CreateIndex(
                name: "IX_NUT_DATA_Deriv_Cd",
                table: "NUT_DATA",
                column: "Deriv_Cd");

            migrationBuilder.CreateIndex(
                name: "IX_NUT_DATA_FoodDescriptionId1",
                table: "NUT_DATA",
                column: "FoodDescriptionId1",
                unique: true,
                filter: "[FoodDescriptionId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NUT_DATA_NDB_No",
                table: "NUT_DATA",
                column: "NDB_No",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NUT_DATA_Nutr_No",
                table: "NUT_DATA",
                column: "Nutr_No");

            migrationBuilder.CreateIndex(
                name: "IX_NUT_DATA_Ref_NDB_No",
                table: "NUT_DATA",
                column: "Ref_NDB_No",
                unique: true,
                filter: "[Ref_NDB_No] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NUT_DATA_Src_Cd",
                table: "NUT_DATA",
                column: "Src_Cd");

            migrationBuilder.CreateIndex(
                name: "IX_WEIGHT_NutrientDataFoodDescriptionId_NutrientDataNutrientDefinitionId",
                table: "WEIGHT",
                columns: new[] { "NutrientDataFoodDescriptionId", "NutrientDataNutrientDefinitionId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DATSRCLN");

            migrationBuilder.DropTable(
                name: "FOOTNOTE");

            migrationBuilder.DropTable(
                name: "LANGUAL");

            migrationBuilder.DropTable(
                name: "WEIGHT");

            migrationBuilder.DropTable(
                name: "DATA_SRC");

            migrationBuilder.DropTable(
                name: "LANGDESC");

            migrationBuilder.DropTable(
                name: "NUT_DATA");

            migrationBuilder.DropTable(
                name: "DERIVCD");

            migrationBuilder.DropTable(
                name: "FOOD_DES");

            migrationBuilder.DropTable(
                name: "NUTR_DEF");

            migrationBuilder.DropTable(
                name: "SRC");

            migrationBuilder.DropTable(
                name: "FD_GROUP");
        }
    }
}
