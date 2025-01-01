using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBSetup.Migrations
{
    /// <inheritdoc />
    public partial class FD_GROUP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FD_GROUP",
                columns: table => new
                {
                    FdGrp_Cd = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false, comment: "4-digit code identifying a food group. Only the first 2 ndigits are currently assigned. In the future, the last 2 digits may be used. Codes may not be consecutive."),
                    FdGrp_Desc = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false, comment: "Name of food group.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FD_GROUP", x => x.FdGrp_Cd);
                },
                comment: "Contains a list of food groups used in SR28 and their descriptions.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FD_GROUP");
        }
    }
}
