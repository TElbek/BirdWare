using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BirdWare.EF.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Familie",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Navn = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Familie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Navn = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Indland = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gruppe",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    FamilieId = table.Column<long>(type: "bigint", nullable: false),
                    Navn = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gruppe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gruppe_Familie_FamilieId",
                        column: x => x.FamilieId,
                        principalTable: "Familie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lokalitet",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    RegionId = table.Column<long>(type: "bigint", nullable: false),
                    Navn = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lokalitet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lokalitet_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Art",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    GruppeId = table.Column<long>(type: "bigint", nullable: false),
                    Navn = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    SU = table.Column<bool>(type: "bit", nullable: false),
                    SetIDK = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Art", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Art_Gruppe_GruppeId",
                        column: x => x.GruppeId,
                        principalTable: "Gruppe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fugletur",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    LokalitetId = table.Column<long>(type: "bigint", nullable: false),
                    Dato = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fugletur", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fugletur_Lokalitet_LokalitetId",
                        column: x => x.LokalitetId,
                        principalTable: "Lokalitet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Observation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtId = table.Column<long>(type: "bigint", nullable: false),
                    FugleturId = table.Column<long>(type: "bigint", nullable: false),
                    Beskrivelse = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Observation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Observation_Art_ArtId",
                        column: x => x.ArtId,
                        principalTable: "Art",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Observation_Fugletur_FugleturId",
                        column: x => x.FugleturId,
                        principalTable: "Fugletur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Art_GruppeId",
                table: "Art",
                column: "GruppeId");

            migrationBuilder.CreateIndex(
                name: "IX_Fugletur_LokalitetId",
                table: "Fugletur",
                column: "LokalitetId");

            migrationBuilder.CreateIndex(
                name: "IX_Gruppe_FamilieId",
                table: "Gruppe",
                column: "FamilieId");

            migrationBuilder.CreateIndex(
                name: "IX_Lokalitet_RegionId",
                table: "Lokalitet",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Observation_ArtId",
                table: "Observation",
                column: "ArtId");

            migrationBuilder.CreateIndex(
                name: "IX_Observation_FugleturId",
                table: "Observation",
                column: "FugleturId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Observation");

            migrationBuilder.DropTable(
                name: "Art");

            migrationBuilder.DropTable(
                name: "Fugletur");

            migrationBuilder.DropTable(
                name: "Gruppe");

            migrationBuilder.DropTable(
                name: "Lokalitet");

            migrationBuilder.DropTable(
                name: "Familie");

            migrationBuilder.DropTable(
                name: "Region");
        }
    }
}
