using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBSetup.Migrations
{
    /// <inheritdoc />
    public partial class DataSourceLink2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DATASRCLN_DATA_SRC_DataSrc_ID",
                table: "DATASRCLN");

            migrationBuilder.DropForeignKey(
                name: "FK_DATASRCLN_FOOD_DES_NDB_No",
                table: "DATASRCLN");

            migrationBuilder.DropForeignKey(
                name: "FK_DATASRCLN_NUTR_DEF_Nutr_No",
                table: "DATASRCLN");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DATASRCLN",
                table: "DATASRCLN");

            migrationBuilder.RenameTable(
                name: "DATASRCLN",
                newName: "DATSRCLN");

            migrationBuilder.RenameIndex(
                name: "IX_DATASRCLN_Nutr_No",
                table: "DATSRCLN",
                newName: "IX_DATSRCLN_Nutr_No");

            migrationBuilder.RenameIndex(
                name: "IX_DATASRCLN_DataSrc_ID",
                table: "DATSRCLN",
                newName: "IX_DATSRCLN_DataSrc_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DATSRCLN",
                table: "DATSRCLN",
                columns: new[] { "NDB_No", "Nutr_No", "DataSrc_ID" });

            migrationBuilder.AddForeignKey(
                name: "FK_DATSRCLN_DATA_SRC_DataSrc_ID",
                table: "DATSRCLN",
                column: "DataSrc_ID",
                principalTable: "DATA_SRC",
                principalColumn: "DataSrc_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DATSRCLN_FOOD_DES_NDB_No",
                table: "DATSRCLN",
                column: "NDB_No",
                principalTable: "FOOD_DES",
                principalColumn: "NDB_No",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DATSRCLN_NUTR_DEF_Nutr_No",
                table: "DATSRCLN",
                column: "Nutr_No",
                principalTable: "NUTR_DEF",
                principalColumn: "Nutr_No",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DATSRCLN_DATA_SRC_DataSrc_ID",
                table: "DATSRCLN");

            migrationBuilder.DropForeignKey(
                name: "FK_DATSRCLN_FOOD_DES_NDB_No",
                table: "DATSRCLN");

            migrationBuilder.DropForeignKey(
                name: "FK_DATSRCLN_NUTR_DEF_Nutr_No",
                table: "DATSRCLN");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DATSRCLN",
                table: "DATSRCLN");

            migrationBuilder.RenameTable(
                name: "DATSRCLN",
                newName: "DATASRCLN");

            migrationBuilder.RenameIndex(
                name: "IX_DATSRCLN_Nutr_No",
                table: "DATASRCLN",
                newName: "IX_DATASRCLN_Nutr_No");

            migrationBuilder.RenameIndex(
                name: "IX_DATSRCLN_DataSrc_ID",
                table: "DATASRCLN",
                newName: "IX_DATASRCLN_DataSrc_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DATASRCLN",
                table: "DATASRCLN",
                columns: new[] { "NDB_No", "Nutr_No", "DataSrc_ID" });

            migrationBuilder.AddForeignKey(
                name: "FK_DATASRCLN_DATA_SRC_DataSrc_ID",
                table: "DATASRCLN",
                column: "DataSrc_ID",
                principalTable: "DATA_SRC",
                principalColumn: "DataSrc_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DATASRCLN_FOOD_DES_NDB_No",
                table: "DATASRCLN",
                column: "NDB_No",
                principalTable: "FOOD_DES",
                principalColumn: "NDB_No",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DATASRCLN_NUTR_DEF_Nutr_No",
                table: "DATASRCLN",
                column: "Nutr_No",
                principalTable: "NUTR_DEF",
                principalColumn: "Nutr_No",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
