using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appmvclibrary.Migrations
{
    /// <inheritdoc />
    public partial class fix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "sachId",
                table: "LichSuMuonTras",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LichSuMuonTras_sachId",
                table: "LichSuMuonTras",
                column: "sachId");

            migrationBuilder.AddForeignKey(
                name: "FK_LichSuMuonTras_Sachs_sachId",
                table: "LichSuMuonTras",
                column: "sachId",
                principalTable: "Sachs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LichSuMuonTras_Sachs_sachId",
                table: "LichSuMuonTras");

            migrationBuilder.DropIndex(
                name: "IX_LichSuMuonTras_sachId",
                table: "LichSuMuonTras");

            migrationBuilder.DropColumn(
                name: "sachId",
                table: "LichSuMuonTras");
        }
    }
}
