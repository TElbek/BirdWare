using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace BirdWare.EF.Migrations
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public partial class lokalitetLatLong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Lokalitet",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Lokalitet",
                type: "float",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SpTripAnalysisResult",
                columns: table => new
                {
                    AnalyseType = table.Column<int>(type: "int", nullable: false),
                    ArtId = table.Column<int>(type: "int", nullable: false),
                    SU = table.Column<bool>(type: "bit", nullable: false),
                    ArtNavn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpTripAnalysisResult");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Lokalitet");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Lokalitet");
        }
    }
}
