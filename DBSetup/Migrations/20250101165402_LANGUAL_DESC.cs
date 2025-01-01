using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBSetup.Migrations
{
    /// <inheritdoc />
    public partial class LANGUAL_DESC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LANGDESC",
                columns: table => new
                {
                    Factor_Code = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false, comment: "The LanguaL factor from the Thesaurus. Only those codes used to factor the foods contained in the LanguaL Factor file are included in this file. "),
                    Description = table.Column<string>(type: "character varying(140)", maxLength: 140, nullable: false, comment: "The description of the LanguaL Factor Code from the thesaurus. ")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LANGDESC", x => x.Factor_Code);
                },
                comment: " This file is a support file to the LanguaL Factor file and contains the descriptions for only those factors used in coding the selected food items codes in this release of SR.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LANGDESC");
        }
    }
}
