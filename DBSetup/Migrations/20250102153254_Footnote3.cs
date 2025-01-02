using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBSetup.Migrations
{
    /// <inheritdoc />
    public partial class Footnote3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FOOTNOTE_NUTR_DEF_Nutr_No",
                table: "FOOTNOTE");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FOOTNOTE",
                table: "FOOTNOTE");

            migrationBuilder.AlterColumn<string>(
                name: "Nutr_No",
                table: "FOOTNOTE",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true,
                comment: "Unique 3-digit identifier code for a nutrient to which footnote applies.",
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3,
                oldComment: "Unique 3-digit identifier code for a nutrient to which footnote applies.");

            migrationBuilder.AddColumn<int>(
                name: "FootnoteId",
                table: "FOOTNOTE",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FOOTNOTE",
                table: "FOOTNOTE",
                column: "FootnoteId");

            migrationBuilder.CreateIndex(
                name: "IX_FOOTNOTE_NDB_No",
                table: "FOOTNOTE",
                column: "NDB_No");

            migrationBuilder.AddForeignKey(
                name: "FK_FOOTNOTE_NUTR_DEF_Nutr_No",
                table: "FOOTNOTE",
                column: "Nutr_No",
                principalTable: "NUTR_DEF",
                principalColumn: "Nutr_No");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FOOTNOTE_NUTR_DEF_Nutr_No",
                table: "FOOTNOTE");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FOOTNOTE",
                table: "FOOTNOTE");

            migrationBuilder.DropIndex(
                name: "IX_FOOTNOTE_NDB_No",
                table: "FOOTNOTE");

            migrationBuilder.DropColumn(
                name: "FootnoteId",
                table: "FOOTNOTE");

            migrationBuilder.AlterColumn<string>(
                name: "Nutr_No",
                table: "FOOTNOTE",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "",
                comment: "Unique 3-digit identifier code for a nutrient to which footnote applies.",
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3,
                oldNullable: true,
                oldComment: "Unique 3-digit identifier code for a nutrient to which footnote applies.");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FOOTNOTE",
                table: "FOOTNOTE",
                columns: new[] { "NDB_No", "Footnt_No", "Nutr_No" });

            migrationBuilder.AddForeignKey(
                name: "FK_FOOTNOTE_NUTR_DEF_Nutr_No",
                table: "FOOTNOTE",
                column: "Nutr_No",
                principalTable: "NUTR_DEF",
                principalColumn: "Nutr_No",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
