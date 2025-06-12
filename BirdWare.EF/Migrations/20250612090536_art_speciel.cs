using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BirdWare.EF.Migrations
{
    /// <inheritdoc />
    public partial class art_speciel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Speciel",
                table: "Art",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Speciel",
                table: "Art");
        }
    }
}
