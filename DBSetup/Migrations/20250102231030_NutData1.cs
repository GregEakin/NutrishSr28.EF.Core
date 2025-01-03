using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBSetup.Migrations
{
    /// <inheritdoc />
    public partial class NutData1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NUT_DATA_NDB_No",
                table: "NUT_DATA");

            migrationBuilder.DropIndex(
                name: "IX_NUT_DATA_Ref_NDB_No",
                table: "NUT_DATA");

            migrationBuilder.CreateIndex(
                name: "IX_NUT_DATA_Ref_NDB_No",
                table: "NUT_DATA",
                column: "Ref_NDB_No");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NUT_DATA_Ref_NDB_No",
                table: "NUT_DATA");

            migrationBuilder.CreateIndex(
                name: "IX_NUT_DATA_NDB_No",
                table: "NUT_DATA",
                column: "NDB_No",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NUT_DATA_Ref_NDB_No",
                table: "NUT_DATA",
                column: "Ref_NDB_No",
                unique: true,
                filter: "[Ref_NDB_No] IS NOT NULL");
        }
    }
}
