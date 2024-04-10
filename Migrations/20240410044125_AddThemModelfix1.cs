using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appmvclibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddThemModelfix1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhieuMuonTras_Sachs_sachId",
                table: "PhieuMuonTras");

            migrationBuilder.AlterColumn<int>(
                name: "sachId",
                table: "PhieuMuonTras",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PhieuMuonTras_Sachs_sachId",
                table: "PhieuMuonTras",
                column: "sachId",
                principalTable: "Sachs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhieuMuonTras_Sachs_sachId",
                table: "PhieuMuonTras");

            migrationBuilder.AlterColumn<int>(
                name: "sachId",
                table: "PhieuMuonTras",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PhieuMuonTras_Sachs_sachId",
                table: "PhieuMuonTras",
                column: "sachId",
                principalTable: "Sachs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
