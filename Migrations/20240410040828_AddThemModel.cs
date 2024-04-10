using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appmvclibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddThemModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LichSuMuonTraId",
                table: "Sachs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LichSuMuonTras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaSinhVien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lop = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichSuMuonTras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhieuMuonTras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaSinhVien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    sachId = table.Column<int>(type: "int", nullable: false),
                    NgayMuon = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTra = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayTaoPhieu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuMuonTras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhieuMuonTras_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PhieuMuonTras_Sachs_sachId",
                        column: x => x.sachId,
                        principalTable: "Sachs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sachs_LichSuMuonTraId",
                table: "Sachs",
                column: "LichSuMuonTraId");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuMuonTras_OrderId",
                table: "PhieuMuonTras",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuMuonTras_sachId",
                table: "PhieuMuonTras",
                column: "sachId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sachs_LichSuMuonTras_LichSuMuonTraId",
                table: "Sachs",
                column: "LichSuMuonTraId",
                principalTable: "LichSuMuonTras",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sachs_LichSuMuonTras_LichSuMuonTraId",
                table: "Sachs");

            migrationBuilder.DropTable(
                name: "LichSuMuonTras");

            migrationBuilder.DropTable(
                name: "PhieuMuonTras");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Sachs_LichSuMuonTraId",
                table: "Sachs");

            migrationBuilder.DropColumn(
                name: "LichSuMuonTraId",
                table: "Sachs");
        }
    }
}
