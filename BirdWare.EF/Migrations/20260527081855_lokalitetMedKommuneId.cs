using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BirdWare.EF.Migrations
{
    /// <inheritdoc />
    public partial class lokalitetMedKommuneId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "KommuneId",
                table: "Lokalitet",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Lokalitet_KommuneId",
                table: "Lokalitet",
                column: "KommuneId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Lokalitet_Kommune_KommuneId",
            //    table: "Lokalitet",
            //    column: "KommuneId",
            //    principalTable: "Kommune",
            //    principalColumn: "Id",
            //    onUpdate: ReferentialAction.NoAction,
            //    onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Lokalitet_Kommune_KommuneId",
            //    table: "Lokalitet");

            migrationBuilder.DropIndex(
                name: "IX_Lokalitet_KommuneId",
                table: "Lokalitet");

            migrationBuilder.DropColumn(
                name: "KommuneId",
                table: "Lokalitet");
        }
    }
}
