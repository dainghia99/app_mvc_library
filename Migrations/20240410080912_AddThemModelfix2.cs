using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appmvclibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddThemModelfix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sachs_LichSuMuonTras_LichSuMuonTraId",
                table: "Sachs");

            migrationBuilder.DropIndex(
                name: "IX_Sachs_LichSuMuonTraId",
                table: "Sachs");

            migrationBuilder.DropColumn(
                name: "LichSuMuonTraId",
                table: "Sachs");

            migrationBuilder.AddColumn<int>(
                name: "LichSuMuonTraId",
                table: "PhieuMuonTras",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhieuMuonTras_LichSuMuonTraId",
                table: "PhieuMuonTras",
                column: "LichSuMuonTraId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhieuMuonTras_LichSuMuonTras_LichSuMuonTraId",
                table: "PhieuMuonTras",
                column: "LichSuMuonTraId",
                principalTable: "LichSuMuonTras",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhieuMuonTras_LichSuMuonTras_LichSuMuonTraId",
                table: "PhieuMuonTras");

            migrationBuilder.DropIndex(
                name: "IX_PhieuMuonTras_LichSuMuonTraId",
                table: "PhieuMuonTras");

            migrationBuilder.DropColumn(
                name: "LichSuMuonTraId",
                table: "PhieuMuonTras");

            migrationBuilder.AddColumn<int>(
                name: "LichSuMuonTraId",
                table: "Sachs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sachs_LichSuMuonTraId",
                table: "Sachs",
                column: "LichSuMuonTraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sachs_LichSuMuonTras_LichSuMuonTraId",
                table: "Sachs",
                column: "LichSuMuonTraId",
                principalTable: "LichSuMuonTras",
                principalColumn: "Id");
        }
    }
}
