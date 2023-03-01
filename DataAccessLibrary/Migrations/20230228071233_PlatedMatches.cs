using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLibrary.Migrations
{
    /// <inheritdoc />
    public partial class PlatedMatches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
            name: "PlayedMatches",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                FirstTeamId = table.Column<int>(type: "int", nullable: false),
                FirstTeamScore = table.Column<int>(type: "int", nullable: false),
                SecondTeamId = table.Column<int>(type: "int", nullable: false),
                SecondTeamScore = table.Column<int>(type: "int", nullable: false),
                Year = table.Column<int>(type: "int", nullable: false),
                CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PlayedMatches", x => x.Id);
                table.ForeignKey(
                    name: "FK_Teams_Teams_FirstTeamId",
                    column: x => x.FirstTeamId,
                    principalTable: "Teams",
                    principalColumn: "Id");

                table.ForeignKey(
                    name: "FK_Teams_Teams_SecondTeamId",
                    column: x => x.SecondTeamId,
                    principalTable: "Teams",
                    principalColumn: "Id");


            });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_FirstTeamId",
                table: "PlayedMatches",
                column: "FirstTeamId");

            migrationBuilder.CreateIndex(
                      name: "IX_TeamsRank_SecondTeamId",
                      table: "PlayedMatches",
                      column: "SecondTeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
              name: "TeamsRank");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
