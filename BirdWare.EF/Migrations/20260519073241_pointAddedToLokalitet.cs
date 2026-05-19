using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace BirdWare.EF.Migrations
{
    /// <inheritdoc />
    public partial class pointAddedToLokalitet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Speciel",
                table: "SpTripAnalysisResult",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FamilieNavn",
                table: "SpHvorKanJegFindeResult",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GruppeNavn",
                table: "SpHvorKanJegFindeResult",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Point>(
                name: "Point",
                table: "Lokalitet",
                type: "geography",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Speciel",
                table: "SpTripAnalysisResult");

            migrationBuilder.DropColumn(
                name: "FamilieNavn",
                table: "SpHvorKanJegFindeResult");

            migrationBuilder.DropColumn(
                name: "GruppeNavn",
                table: "SpHvorKanJegFindeResult");

            migrationBuilder.DropColumn(
                name: "Point",
                table: "Lokalitet");
        }
    }
}
