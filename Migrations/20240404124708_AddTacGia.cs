using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace appmvclibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddTacGia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TacGias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TieuSu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TacGias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TacGiaSachs",
                columns: table => new
                {
                    SachId = table.Column<int>(type: "int", nullable: false),
                    TacGiaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TacGiaSachs", x => new { x.SachId, x.TacGiaId });
                    table.ForeignKey(
                        name: "FK_TacGiaSachs_Sachs_SachId",
                        column: x => x.SachId,
                        principalTable: "Sachs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TacGiaSachs_TacGias_TacGiaId",
                        column: x => x.TacGiaId,
                        principalTable: "TacGias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TacGiaSachs_TacGiaId",
                table: "TacGiaSachs",
                column: "TacGiaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TacGiaSachs");

            migrationBuilder.DropTable(
                name: "TacGias");
        }
    }
}
