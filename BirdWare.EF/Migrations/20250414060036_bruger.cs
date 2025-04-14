using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BirdWare.EF.Migrations
{
    /// <inheritdoc />
    public partial class bruger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bruger",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bruger", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpHvorKanJegFindeResult",
                columns: table => new
                {
                    ArtId = table.Column<long>(type: "bigint", nullable: false),
                    ArtNavn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LokalitetNavn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    Distance = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "SpLokaliteterByLatLongResult",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Distance = table.Column<double>(type: "float", nullable: false),
                    Navn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AntalTure = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bruger");

            migrationBuilder.DropTable(
                name: "SpHvorKanJegFindeResult");

            migrationBuilder.DropTable(
                name: "SpLokaliteterByLatLongResult");
        }
    }
}
